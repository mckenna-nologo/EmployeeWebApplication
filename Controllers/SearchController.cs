using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class SearchController : Controller
    {
        
        private EmployeeDataViewModel model = new EmployeeDataViewModel();

        public SearchController()
        {
            model.Employees = EmployeeMockData.Employees;
        }

        public IActionResult SearchView()
        {
            model.HasSearched = false;

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(string name, string department)
        {
            var searchName = name?.Trim() ?? string.Empty; //trim whitespaces, if null make it an empty string
            var searchDepartment = department?.Trim() ?? string.Empty;

            List<Employee> searchResults = EmployeeMockData.Employees
                .Where(employee =>
                    (string.IsNullOrEmpty(searchName) ||
                        employee.FirstName.Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
                        employee.LastName.Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
                        ($"{employee.FirstName} {employee.LastName}").Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
                        ($"{employee.LastName} {employee.FirstName}").Contains(searchName, StringComparison.OrdinalIgnoreCase)
                    ) &&
                    (string.IsNullOrEmpty(searchDepartment) || employee.Department.Contains(searchDepartment, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();

            model.SearchResults = searchResults;
            model.HasSearched = true;

            return View("SearchView", model);
        }
    }
}
