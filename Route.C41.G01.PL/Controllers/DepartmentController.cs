using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BBL.Interfaces;
using Route.C41.G01.DAL.Models;
using System;
namespace Route.C41.G01.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        // Inheritance : DepartmentRepository is a Controller
        // Composation : DepartmentController has a DepartmentRepository


        public DepartmentController(IDepartmentRepository departmentRepo, IWebHostEnvironment env) // Ask CLR for creating an object from class implementing "IDepartmentRepository" Interface
        {

            _departmentRepo = departmentRepo;
            _env = env;
        }



        // Department/Index

        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll();
            return View(departments);
        }

        // /Depertment/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var count = _departmentRepo.Add(department);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = _departmentRepo.Get(id.Value);

            if (department is null)
                return NotFound(); // 404

            return View(viewName, department);

        }

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
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if (id != department.Id)
                return BadRequest();
                //return BadRequest("An Error Ya Hamadaaaaaaa !!");



            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _departmentRepo.Update(department);
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

                return View(department);
            }
        }
    }
}
