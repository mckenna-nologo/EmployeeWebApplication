using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class CreateController : Controller
    {
        public IActionResult CreateView()
        {
            return View();
        }

        //create employee
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var fields = new Dictionary<string, string>
        {
            { "First name", employee.FirstName },
            { "Last name", employee.LastName },
            { "Email", employee.Email },
            { "Department", employee.Department }
        };

            foreach (var field in fields) //no field must be empty validation message
            {
                if (string.IsNullOrWhiteSpace(field.Value))
                {
                    TempData["ErrorMessage"] = field.Key + " is required.";
                    return RedirectToAction("Index", "Home");
                }
            }

            employee.EmployeeId = EmployeeMockData.GetNextId();
            employee.DateCreated = DateTime.Now;

            EmployeeMockData.Employees.Add(employee);

            TempData["SuccessMessage"] = "Employee successfully created!";
            return RedirectToAction("Employees", "Home");

        }
    }
}
