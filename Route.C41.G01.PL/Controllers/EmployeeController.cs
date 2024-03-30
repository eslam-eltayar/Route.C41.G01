﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Route.C41.G01.BBL.Interfaces;
using Route.C41.G01.DAL.Models;
using Route.C41.G01.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route.C41.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepo;
        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IMapper mapper,IEmployeeRepository employeeRepo, IWebHostEnvironment env) // Ask CLR for creating an object from class implementing "IDepartmentRepository" Interface
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            _env = env;
            //_departmentRepo = departmentRepo;
        }

        // /Emplyee/Index
        //[HttpGet]
        public IActionResult Index(string searchInp)
        {
            //TempData.Keep();
            // Binding Through View's Dictionary => Transfer Data From Action To View [One Way]

            // 1. ViewData => Dictionary object -> Key Value Pair 
            ViewData["Message"] = "Hello ViewData";


            // 2. ViewBag => Dynamic Property -> Key Value Pair 
            ViewBag.Message = "Hello ViewBag";

            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(searchInp))
                employees = _employeeRepo.GetAll();
            else
                employees = _employeeRepo.SearchByName(searchInp.ToLower());


            var mappedEmps = _mapper.Map<IEnumerable<Employee> , IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmps);  

        }

        // Create 
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                // Manual Mapping
                ///var mappedEmp = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Age = employeeVM.Age,
                ///    Address = employeeVM.Address,
                ///    Salary = employeeVM.Salary,
                ///    Email = employeeVM.Email,
                ///    PhoneNumber = employeeVM.PhoneNumber,
                ///    IsActive = employeeVM.IsActive,
                ///    HiringDate = employeeVM.HiringDate,
                ///
                ///};
                ///

                ///Employee mappedEmp = (Employee)employeeVM;
                ///

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                var count = _employeeRepo.Add(mappedEmp);

                // 3. TempData

                if (count > 0)
                    TempData["Message"] = "Department is Created Successfuly";
                else
                    TempData["Message"] = "An Error has Occured, Department Not Created :(";

                return RedirectToAction(nameof(Index));
            }

            return View(employeeVM);
        }

        // Details

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var employee = _employeeRepo.Get(id.Value);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();

            return View(viewName, mappedEmp);
        }

        // Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            //return BadRequest("An Error Ya Hamadaaaaaaa !!");

            if (!ModelState.IsValid)
                return View(employeeVM);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepo.Update(mappedEmp);
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

                return View(employeeVM);
            }
        }

        // Delete
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepo.Delete(mappedEmp);
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

                return View(employeeVM);
            }

        }

    }
}
