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

        public IActionResult Employees(string sortField, string sortDir)
        {
            //show the current selection to the view
            ViewBag.SortField = sortField ?? string.Empty;
            ViewBag.SortDir = sortDir ?? "asc";

            var employees = model.Employees.AsQueryable();
            employees = ApplySorting(employees, sortField, sortDir);

            model.Employees = employees.ToList();
            return View(model);
        }

        private IQueryable<Employee> ApplySorting(IQueryable<Employee> source, string sortField, string sortDir)
        {
            var field = (sortField ?? "").ToLowerInvariant();
            var dir = (sortDir ?? "asc").ToLowerInvariant();

            switch (field)
            {
                case "first":
                case "firstname":
                    return dir == "desc" ? source.OrderByDescending(e => e.FirstName) : source.OrderBy(e => e.FirstName);
                case "last":
                case "lastname":
                    return dir == "desc" ? source.OrderByDescending(e => e.LastName) : source.OrderBy(e => e.LastName);
                case "email":
                    return dir == "desc" ? source.OrderByDescending(e => e.Email) : source.OrderBy(e => e.Email);
                case "department":
                case "dept":
                    return dir == "desc" ? source.OrderByDescending(e => e.Department) : source.OrderBy(e => e.Department);
                case "date":
                case "datecreated":
                    return dir == "desc" ? source.OrderByDescending(e => e.DateCreated) : source.OrderBy(e => e.DateCreated);
                default:
                    return dir == "desc" ? source.OrderByDescending(e => e.EmployeeId) : source.OrderBy(e => e.EmployeeId);
            }
        }
    }
}