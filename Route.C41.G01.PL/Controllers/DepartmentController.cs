using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BBL.Interfaces;
using Route.C41.G01.DAL.Models;
using Route.C41.G01.PL.ViewModels;
using System;
using System.Collections.Generic;
namespace Route.C41.G01.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        // Inheritance : DepartmentRepository is a Controller
        // Composation : DepartmentController has a DepartmentRepository


        public DepartmentController(IMapper mapper,IDepartmentRepository departmentRepo, IWebHostEnvironment env) // Ask CLR for creating an object from class implementing "IDepartmentRepository" Interface
        {
            _mapper = mapper;
            _departmentRepo = departmentRepo;
            _env = env;
        }



        // Department/Index

        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll();
            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDep);
        }


        // Create 

        // /Depertment/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                var count = _departmentRepo.Add(mappedDep);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        // Details

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = _departmentRepo.Get(id.Value);

            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);


            if (department is null)
                return NotFound(); // 404

            return View(viewName, mappedDep);

        }
        //Edit

        // /Department/Edit/10
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

            /// if (!id.HasValue)
            ///     return BadRequest();
            /// var department = _departmentRepo.Get(id.Value);
            /// if (department is null)
            ///     return NotFound();
            /// return View();
            ///
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            //return BadRequest("An Error Ya Hamadaaaaaaa !!");

            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _departmentRepo.Update(mappedDep);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Execption
                // 2. Freindly Message 

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message); // Add Custom Error
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department!");

                return View(departmentVM);
            }
        }

        // Delete
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");

        }


        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _departmentRepo.Delete(mappedDep);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Execption
                // 2. Freindly Message 

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message); // Add Custom Error
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department!");

                return View(departmentVM);
            }

        }
    }
}
