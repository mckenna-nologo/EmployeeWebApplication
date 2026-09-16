using EmployeeWebApplication.Helpers;
using EmployeeWebApplication.Interfaces;
using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
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
        public IActionResult Create(Employee employee)
        {
            //validation check for the employee to see if the model is valid, if not, return to the CreateView with the employee data
            if (!ModelState.IsValid)
            {
                return View("CreateView", employee);//keep the information the user entered
            }

            _employeeHelper.Create(employee);

            TempData["SuccessMessage"] = "Employee successfully created!";

            return RedirectToAction("Employees", "Home");
        }
    }
}