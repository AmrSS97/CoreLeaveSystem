using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreLeaveSystem.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreLeaveSystem.Controllers
{
    public class VacationAllocationController : Controller
    {
        private readonly IVacationTypeRepository _typerepo;
        private readonly IVacationAllocationRepository _allocationrepo;
        private readonly IMapper _mapper;


        // GET: VacationAllocationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VacationAllocationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            return View();
        }

        // POST: VacationAllocationController/Edit/5
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

        // GET: VacationAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VacationAllocationController/Delete/5
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
