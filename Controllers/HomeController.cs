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
        public IActionResult Employees(string sortField, string sortDir, string filter, string statusFilter, int page = 1, int pageSize = 10)
        {
            //show the current selection to the view
            ViewBag.SortField = sortField ?? string.Empty;
            ViewBag.SortDir = sortDir ?? "asc";
            ViewBag.Filter = filter ?? string.Empty;
            ViewBag.StatusFilter = statusFilter ?? "all";

            var employees = model.Employees.AsQueryable();

           if (!string.IsNullOrWhiteSpace(filter))
            {
                var f = filter.Trim();
                var fl = f.ToLowerInvariant();
                employees = employees.Where(e =>
                    (!string.IsNullOrEmpty(e.FirstName) && e.FirstName.ToLowerInvariant().Contains(fl)) ||
                    (!string.IsNullOrEmpty(e.LastName) && e.LastName.ToLowerInvariant().Contains(fl)) ||
                    (!string.IsNullOrEmpty(e.Email) && e.Email.ToLowerInvariant().Contains(fl)) ||
                    (!string.IsNullOrEmpty(e.Department) && e.Department.ToLowerInvariant().Contains(fl)) ||
                    ($"{(e.FirstName ?? string.Empty)} {(e.LastName ?? string.Empty)}").ToLowerInvariant().Contains(fl) ||
                    ($"{(e.LastName ?? string.Empty)} {(e.FirstName ?? string.Empty)}").ToLowerInvariant().Contains(fl)
                );
            }

            //status filter
            if (!string.IsNullOrWhiteSpace(statusFilter))
            {
                var sf = statusFilter.Trim().ToLowerInvariant();
                if (sf == "active")
                {
                    employees = employees.Where(e => e.IsActive);
                }
                else if (sf == "inactive")
                {
                    employees = employees.Where(e => !e.IsActive);
                }
            }

            employees = ApplySorting(employees, sortField, sortDir);

            //pagination
            var totalItems = employees.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var paged = employees.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            model.Employees = paged;
            model.PageNumber = page;
            model.PageSize = pageSize;
            model.TotalItems = totalItems;
            model.TotalPages = totalPages;

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