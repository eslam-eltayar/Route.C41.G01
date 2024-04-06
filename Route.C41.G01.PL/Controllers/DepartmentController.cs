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
using System.Threading.Tasks;
namespace Route.C41.G01.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        // Inheritance : DepartmentRepository is a Controller
        // Composation : DepartmentController has a DepartmentRepository


        public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment env) // Ask CLR for creating an object from class implementing "IDepartmentRepository" Interface
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }



        // Department/Index

        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.Repository<Department>().GetAllAsync();

            var mappedDepts = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepts);
        }


        // Create 

        // /Depertment/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var mappedDepts = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.Repository<Department>().Add(mappedDepts);

                var count = await _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        // Details

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = await _unitOfWork.Repository<Department>().GetAsync(id.Value);

            var mappedDepts = _mapper.Map<Department, DepartmentViewModel>(department);

            if (department is null)
                return NotFound(); // 404

            return View(viewName, mappedDepts);

        }
        //Edit

        // /Department/Edit/10
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");

            /// if (!id.HasValue)
            ///     return BadRequest();
            /// var department = _unitOfWork.Repository<Department>().Get(id.Value);
            /// if (department is null)
            ///     return NotFound();
            /// return View();
            ///
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            //return BadRequest("An Error Ya Hamadaaaaaaa !!");

            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var mappedDepts = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.Repository<Department>().Update(mappedDepts);
                await _unitOfWork.Complete();
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }


        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedDepts = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.Repository<Department>().Delete(mappedDepts);
                var count = await _unitOfWork.Complete();

                if(count > 0)
                return RedirectToAction(nameof(Index));

                return View(departmentVM);
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
