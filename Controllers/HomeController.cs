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
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var employees = from emp in model.Employees
                            select emp; 

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.LastName); break;
                case "Date":
                    employees = employees.OrderBy(e => e.DateCreated); break;
                case "date_desc":
                    employees = employees.OrderByDescending(e => e.DateCreated); break;
                default:
                    employees = employees.OrderBy(e => e.EmployeeId); break;

            }

            model.Employees = employees.ToList();
            return View(model);
        }
    }
}