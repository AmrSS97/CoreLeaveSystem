﻿using System;
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
        public async Task<ActionResult> Index()
        {
            var vacationtypes =await  _typerepo.FindAll();
            var mappedvacationtypes = _mapper.Map<List<VacationType>, List<VacationTypeVM>>(vacationtypes.ToList());
            var model = new CreateVacationAllocationVM
            {
                VacationTypes = mappedvacationtypes,
                NumberUpdated = 0
            };
            return View(model);

        }

        public async Task<ActionResult> SetLeave(int id)
        {
            var vacationtype = await _typerepo.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            foreach (var emp in employees)
            {
                var checkAllocation = await _allocationrepo.CheckAllocation(id, emp.Id);
                if (checkAllocation)
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
                await _allocationrepo.Create(vacationallocation);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> ListEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var model = _mapper.Map<List<EmployeeVM>>(employees);
            return View(model);
        }
        // GET: VacationAllocationController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var employee = _mapper.Map<EmployeeVM>(await _userManager.FindByIdAsync(id));
            var allocations = _mapper.Map<List<VacationAllocationVM>>(await _allocationrepo.GetVacationAllocationsByEmployee(id));
            var model = new ViewAllocationVM
            {
                Employee = employee,
                VacationAllocations = allocations
            };

            return View(model);
        }
        // GET: VacationAllocationController/EditEmployee
        public async Task<ActionResult> EditEmployee(string id)
        {
            var employee = await _employee.FindByIdStringAsync(id);
            var model = _mapper.Map<EmployeeVM>(employee);
            return View(model);

        }
        // POST: VacationAllocationController/EditEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEmployee(EmployeeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }
                var Employee = _mapper.Map<Employee>(model);
                var isSuccess = await _employee.Update(Employee);
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
        public async Task<ActionResult> Edit(int id)
        {
            var vacationallocation = await _allocationrepo.FindById(id);
            var model = _mapper.Map<EditVacationAllocationVM>(vacationallocation);
            return View(model);
        }

        // POST: VacationAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditVacationAllocationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var record = await _allocationrepo.FindById(model.Id);
                record.NumberOfDays = model.NumberOfDays;
                var isSucces = await _allocationrepo.Update(record);
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
        public async Task<ActionResult> DeleteEmployee(string id)
        {

            var employee = await _employee.FindByIdStringAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var isSuccess = await _employee.Delete(employee);
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
        public async Task<ActionResult> DeleteEmployee(string id, EmployeeVM model)
        {
            try
            {
                var employee =await _employee.FindByIdStringAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                var isSuccess = await _employee.Delete(employee);
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
