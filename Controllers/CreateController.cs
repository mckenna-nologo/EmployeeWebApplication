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
                    return Content("<script>alert('" + field.Key + " is required.'); window.location.href='/Home/Index';</script>", "text/html");
                    //referenced from: https: //www.w3schools.com/js/js_window_location.asp
                }
            }

            employee.EmployeeId = EmployeeMockData.GetNextId();
            employee.DateCreated = DateTime.Now;

            EmployeeMockData.Employees.Add(employee);

            return Content("<script>alert('Employee successfully created!'); window.location.href='/Home/Index';</script>", "text/html");

        }
    }
}
