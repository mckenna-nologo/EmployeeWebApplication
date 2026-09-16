using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class CreateController : Controller
    {
        public IActionResult CreateView()
        {
            return View(new Employee());
        }

        //create employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            // Server-side validation using data annotations
            if (!ModelState.IsValid)
            {
                // return the create view with the submitted employee so validation messages show
                return View("CreateView", employee);
            }

            employee.EmployeeId = EmployeeMockData.GetNextId();
            employee.DateCreated = DateTime.Now;

            EmployeeMockData.Employees.Add(employee);

            TempData["SuccessMessage"] = "Employee successfully created!";
            return RedirectToAction("Employees", "Home");

        }
    }
}
