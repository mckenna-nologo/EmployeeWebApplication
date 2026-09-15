using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult SearchView()
        {
            EmployeeDataViewModel model = new EmployeeDataViewModel();
            model.Employees = EmployeeMockData.Employees;
            model.HasSearched = false;

            return View(model);
        }

        //search through employee data
        [HttpPost]
        public IActionResult Search(string name, string department)
        {
            List<Employee> searchResults = EmployeeMockData.Employees
                .Where(employee => //where a particular employee
                (string.IsNullOrEmpty(name) || employee.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase)) &&  //the name is either null and the department is entered
                (string.IsNullOrEmpty(department) || employee.Department.Contains(department, StringComparison.OrdinalIgnoreCase)) //or the department is null and the name is entered
                )
                .ToList(); //put the results in a list 


            EmployeeDataViewModel model = new EmployeeDataViewModel();

            model.Employees = EmployeeMockData.Employees; //not working
            model.SearchResults = searchResults;
            model.HasSearched = true;

            // return SearchView with search results
            return View("SearchView", model);
        }
    }
}
