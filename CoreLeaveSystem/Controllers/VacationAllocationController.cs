using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
using CoreLeaveSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreLeaveSystem.Controllers
{
    public class VacationAllocationController : Controller
    {
        private readonly IVacationTypeRepository _typerepo;
        private readonly IVacationAllocationRepository _allocationrepo;
        private readonly IEmployeeRepository _employee;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;


        public VacationAllocationController(IVacationTypeRepository typerepo, IVacationAllocationRepository allocationrepo, IMapper mapper, UserManager<Employee> userManager, IEmployeeRepository employee)
        {
            _typerepo = typerepo;
            _allocationrepo = allocationrepo;
            _mapper = mapper;
            _userManager = userManager;
            _employee = employee;
        }

        // GET: VacationAllocationController
        public ActionResult Index()
        {
            var vacationtypes = _typerepo.FindAll().ToList();
            var mappedvacationtypes = _mapper.Map<List<VacationType>, List<VacationTypeVM>>(vacationtypes);
            var model = new CreateVacationAllocationVM
            {
                VacationTypes = mappedvacationtypes,
                NumberUpdated = 0
            };
            return View(model);

        }

        public ActionResult SetLeave(int id)
        {
            var vacationtype = _typerepo.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            foreach (var emp in employees)
            {
                if (_allocationrepo.CheckAllocation(id, emp.Id))
                    continue;
                var allocation = new VacationAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    VacationTypeId = id,
                    NumberOfDays = vacationtype.Balance,
                    Period = DateTime.Now.Year
                };
                var vacationallocation = _mapper.Map<VacationAllocation>(allocation);
                _allocationrepo.Create(vacationallocation);
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _mapper.Map<List<EmployeeVM>>(employees);
            return View(model);
        }
        // GET: VacationAllocationController/Details/5
        public ActionResult Details(string id)
        {
            var employee = _mapper.Map<EmployeeVM>(_userManager.FindByIdAsync(id).Result);
            var allocations = _mapper.Map<List<VacationAllocationVM>>(_allocationrepo.GetVacationAllocationsByEmployee(id));
            var model = new ViewAllocationVM
            {
                Employee = employee,
                VacationAllocations = allocations
            };

            return View(model);
        }
        // GET: VacationAllocationController/EditEmployee
        public ActionResult EditEmployee(string id)
        {
            var employee = _employee.FindByIdString(id);
            var model = _mapper.Map<EmployeeVM>(employee);
            return View(model);

        }
        // POST: VacationAllocationController/EditEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EmployeeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var Employee = _mapper.Map<Employee>(model);
                var isSuccess = _employee.Update(Employee);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(ListEmployees));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(model);
            }
        }

        // GET: VacationAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VacationAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VacationAllocationController/Edit/5
        public ActionResult Edit(int id)
        {
            var vacationallocation = _allocationrepo.FindById(id);
            var model = _mapper.Map<EditVacationAllocationVM>(vacationallocation);
            return View(model);
        }

        // POST: VacationAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditVacationAllocationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var record = _allocationrepo.FindById(model.Id);
                record.NumberOfDays = model.NumberOfDays;
                var isSucces = _allocationrepo.Update(record);
                if (!isSucces)
                {
                    ModelState.AddModelError("", "Error while saving...");
                    return View(model);
                }
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch
            {
                return View();
            }
        }

        // GET: VacationAllocationController/Delete/5
        public ActionResult DeleteEmployee(string id)
        {

            var employee = _employee.FindByIdString(id);
            if (employee == null)
            {
                return NotFound();
            }
            var isSuccess = _employee.Delete(employee);
            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return BadRequest();
            }
            return RedirectToAction(nameof(ListEmployees));
        }

        // POST: VacationAllocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployee(string id, EmployeeVM model)
        {
            try
            {
                var employee = _employee.FindByIdString(id);
                if (employee == null)
                {
                    return NotFound();
                }
                var isSuccess = _employee.Delete(employee);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(ListEmployees));
            }
            catch
            {
                return View();
            }
    }
    }
}
