using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BBL.Interfaces;
using Route.C41.G01.DAL.Models;
using System;

namespace Route.C41.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        
        private readonly IWebHostEnvironment _env;
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo, IWebHostEnvironment env) // Ask CLR for creating an object from class implementing "IDepartmentRepository" Interface
        {
            _employeeRepo = employeeRepo;
            _env = env;
        }

        // /Emplyee/Index
        public IActionResult Index()
        {
            var employyes = _employeeRepo.GetAll();
            return View();
        }

        // Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // Details

        public IActionResult Details(int? id, string viewName= "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var employee = _employeeRepo.Get(id.Value);

            if (employee is null)
                return NotFound();

            return View(viewName, employee);
        }

        // Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            //return BadRequest("An Error Ya Hamadaaaaaaa !!");

            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                _employeeRepo.Update(employee);
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

                return View(employee);
            }
        }

        // Delete
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepo.Delete(employee);
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

                return View(employee);
            }

        }

    }
}
