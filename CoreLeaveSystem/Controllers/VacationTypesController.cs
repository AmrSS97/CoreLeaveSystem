using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreLeaveSystem.Contracts;
using CoreLeaveSystem.Data;
using CoreLeaveSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreLeaveSystem.Controllers
{
    public class VacationTypesController : Controller
    {
        private readonly IVacationTypeRepository _repo;
        private readonly IMapper _mapper;

        public VacationTypesController(IVacationTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: VacationTypesController
        public ActionResult Index()
        {
            var vacationtypes = _repo.FindAll().ToList();
            var model = _mapper.Map<List<VacationType>, List<VacationTypeVM>>(vacationtypes);
            return View(model);
        }

        // GET: VacationTypesController/Details/5
        public ActionResult Details(int id)
        {
            if(!_repo.isExists(id))
            {
                return NotFound();
            }
            var vacationType = _repo.FindById(id);
            var model = _mapper.Map<VacationTypeVM>(vacationType);
            return View(model);
        }

        // GET: VacationTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VacationTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VacationTypeVM model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                var vacationType = _mapper.Map<VacationType>(model);
                vacationType.DateCreated = DateTime.Now;
                var isSuccess = _repo.Create(vacationType);
                if(!isSuccess)
                {
                    ModelState.AddModelError("","Something Went Wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VacationTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            if(!_repo.isExists(id))
            {
                return NotFound();
            }
            var vacationtype = _repo.FindById(id);
            var model = _mapper.Map<VacationTypeVM>(vacationtype);
            return View(model);
        }

        // POST: VacationTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VacationTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var vacationType = _mapper.Map<VacationType>(model);
                var isSuccess = _repo.Update(vacationType);
                if(!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(model);
            }
        }

        // GET: VacationTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            var vacationtype = _repo.FindById(id);
            if (vacationtype == null)
            {
                return NotFound();
            }
            var isSuccess = _repo.Delete(vacationtype);
            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: VacationTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VacationTypeVM model)
        {
            try
            {
                var vacationtype = _repo.FindById(id);
                if(vacationtype == null)
                {
                    return NotFound();
                }
                var isSuccess = _repo.Delete(vacationtype);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
