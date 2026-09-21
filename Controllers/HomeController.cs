using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeWebApplication.Controllers
{
    public class HomeController : Controller
    {
        //solid + interfaces

        private EmployeeDataViewModel model = new EmployeeDataViewModel(); //fields 

        public HomeController() //constructor
        {
            model.Employees = EmployeeMockData.Employees;
        }

        public IActionResult Index()
        {
            return View(model);
        }

        public IActionResult Employees(string sortOrder)
        {
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name_asc" : "";
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name_asc" : "";
            ViewBag.DepartmentSortParm = String.IsNullOrEmpty(sortOrder) ? "department_asc" : "";
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_asc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var employees = from emp in model.Employees
                            select emp; 

            switch (sortOrder)
            {
                case "last_name_asc":
                    employees = employees.OrderBy(e => e.LastName); break;
                case "first_name_asc":
                    employees = employees.OrderBy(e => e.FirstName); break;
                case "department_asc":
                    employees = employees.OrderBy(e => e.Department); break;
                case "email_asc":
                    employees = employees.OrderBy(e => e.Email); break;
                case "Date":
                    employees = employees.OrderBy(e => e.DateCreated); break;
                case "date_asc":
                    employees = employees.OrderBy(e => e.DateCreated); break;
                default:
                    employees = employees.OrderBy(e => e.EmployeeId); break;

            }

            model.Employees = employees.ToList();
            return View(model);
        }
    }
}