using EmployeeWebApplication.Helpers;
using EmployeeWebApplication.Interfaces;
using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class CreateController : Controller
    {
        private readonly IEmployeeHelper _employeeHelper;

        public CreateController(IEmployeeHelper employeeHelper)
        {
            _employeeHelper = employeeHelper;
        }

        public IActionResult CreateView()
        {
            return View(new Employee());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            //validation check for the employee to see if the model is valid, if not, return to the CreateView with the employee data
            if (!ModelState.IsValid)
            {
                return View("CreateView", employee);//keep the information the user entered
            }

            var first = (employee.FirstName ?? string.Empty).Trim();
            var last = (employee.LastName ?? string.Empty).Trim();
            var email = (employee.Email ?? string.Empty).Trim();

            //check for existing employee
            var alreadyExists = EmployeeMockData.Employees
                .Any(tempEmployee => !string.IsNullOrEmpty(email) && //if the email is not null or empty
                tempEmployee.Email.Equals(email) ||
                (!string.IsNullOrEmpty(first) && !string.IsNullOrEmpty(last) &&
                 tempEmployee.FirstName.Equals(first) && tempEmployee.LastName.Equals(last))
                );


            if (alreadyExists)
            {
                TempData["ErrorMessage"] = "An employee with the same name or email already exists";
                return View("CreateView", employee);
            }

            _employeeHelper.Create(employee);

            TempData["SuccessMessage"] = "Employee successfully created!";

            return RedirectToAction("Employees", "Home");
        }
    }
}