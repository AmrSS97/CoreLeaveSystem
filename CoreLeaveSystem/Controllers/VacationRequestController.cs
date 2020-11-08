using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
using CoreLeaveSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreLeaveSystem.Controllers
{
    [Authorize]
    public class VacationRequestController : Controller
    {
        private readonly IVacationRequestRepository _requestrepo;
        private readonly IVacationTypeRepository _typerepo;
        private readonly IVacationAllocationRepository _allocationrepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public VacationRequestController(IVacationRequestRepository requestrepo, IMapper mapper, UserManager<Employee> userManager, IVacationTypeRepository typerepo, IVacationAllocationRepository allocationrepo )
        {
            _requestrepo = requestrepo;
            _mapper = mapper;
            _userManager = userManager;
            _typerepo = typerepo;
            _allocationrepo = allocationrepo;
        }

        [Authorize(Roles = "Administrator")]
        // GET: VacationRequestController
        public ActionResult Index()
        {
            var vacationRequests = _requestrepo.FindAll();
            var vacationRequestsModel = _mapper.Map<List<VacationRequestVM>>(vacationRequests);
            var model = new AdminVacationRequestViewVM
            {
                TotalRequests = vacationRequestsModel.Count,
                ApprovedRequests = vacationRequestsModel.Count(q => q.Approved == true),
                PendingRequests = vacationRequestsModel.Count(q => q.Approved == null),
                RejectedRequests = vacationRequestsModel.Count(q => q.Approved == false),
                VacationRequests = vacationRequestsModel
            };
            return View(model);
        }

        // GET: VacationRequestController/Details/5
        public ActionResult Details(int id)
        {
            var vacationrequest = _requestrepo.FindById(id);
            var model = _mapper.Map<VacationRequestVM>(vacationrequest);

            return View(model);
        }

        public ActionResult ApproveRequest(int id)
        {

            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var vacationRequest = _requestrepo.FindById(id);
                var employeeid = vacationRequest.RequestingEmployeeId;
                var vacationtypeId = vacationRequest.VacationTypeId;
                var allocation = _allocationrepo.GetVacationAllocationsByEmployeeAndType(employeeid,vacationtypeId);
                int daysRequested = (int)(vacationRequest.EndDate - vacationRequest.StartDate).TotalDays;
                // allocation.NumberOfDays = allocation.NumberOfDays - daysRequested; Alternative Way
                allocation.NumberOfDays -= daysRequested;

                vacationRequest.Approved = true;
                vacationRequest.ApprovedById = user.Id;
                vacationRequest.DateActioned = DateTime.Now;

                 _requestrepo.Update(vacationRequest);
                _allocationrepo.Update(allocation);
                return RedirectToAction(nameof(Index), "Home");

            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index), "Home");
            }
          
        }

        public ActionResult RejectRequest(int id)
        {

            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var vacationrequest = _requestrepo.FindById(id);
                vacationrequest.Approved = false;
                vacationrequest.ApprovedById = user.Id;
                vacationrequest.DateActioned = DateTime.Now;

                _requestrepo.Update(vacationrequest);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult CancelRequest(int id)
        {
            var vacationRequest = _requestrepo.FindById(id);
            vacationRequest.Cancelled = true;
            _requestrepo.Update(vacationRequest);
            return RedirectToAction(nameof(MyLeave));
        }

        public ActionResult MyLeave()
        {
            var employee = _userManager.GetUserAsync(User).Result;
            var employeeid = employee.Id;
            var employeeAllocations = _allocationrepo.GetVacationAllocationsByEmployee(employeeid);
            var employeeRequests = _requestrepo.GetVacationRequestByEmployee(employeeid);

            var employeeAllocationsModel = _mapper.Map<List<VacationAllocationVM>>(employeeAllocations);
            var employeeRequestsModel = _mapper.Map<List<VacationRequestVM>>(employeeRequests);

            var model = new EmployeeVacationRequestViewVM
            {
                VacationAllocations = employeeAllocationsModel,
                VacationRequests = employeeRequestsModel
            };

            return View(model);
        }

        // GET: VacationRequestController/Create
        public ActionResult Create()
        {
            var vacationtypes = _typerepo.FindAll();
            var vacationtypeItems = vacationtypes.Select(q => new SelectListItem { 
            Text = q.Name,
            Value = q.Id.ToString()
            });
            var model = new CreateVacationRequestVM
            {
                VacationTypes = vacationtypeItems
            };
            return View(model);
        }

        // POST: VacationRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVacationRequestVM model)
        {
            var startDate = Convert.ToDateTime(model.StartDate);
            var endDate = Convert.ToDateTime(model.EndDate);
            var vacationtypes = _typerepo.FindAll();
            var vacationtypeItems = vacationtypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            model.VacationTypes = vacationtypeItems;
            try
            {
               if(!ModelState.IsValid)
                {
                    return View(model);
                }

               if(DateTime.Compare(startDate,endDate) > 1)
                {
                    ModelState.AddModelError("", "Start Date cannot be further in the fiuture than the End Date...");
                    return View(model);
                }

                var employee = _userManager.GetUserAsync(User).Result;
                var allocation = _allocationrepo.GetVacationAllocationsByEmployeeAndType(employee.Id, model.VacationTypeId);
                int daysRequested = (int)(endDate - startDate).TotalDays;

                if(daysRequested > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You don't have sufficient days for this request...");
                    return View(model);
                }

                var VacationRequestModel = new VacationRequestVM
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    Approved = null,
                    Cancelled = false,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    VacationTypeId = model.VacationTypeId
                };

                var vacationrequest = _mapper.Map<VacationRequest>(VacationRequestModel);
                var isSuccess = _requestrepo.Create(vacationrequest);

                if(!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong when submitting your record...");
                    return View(model);
                }

                return RedirectToAction(nameof(MyLeave));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: VacationRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VacationRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: VacationRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VacationRequestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
