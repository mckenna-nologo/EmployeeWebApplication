using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class UpdateController : Controller
    {

        public IActionResult UpdateView()
        {
            // Ensure the UpdateView always receives a non-null model to avoid null reference in the Razor page
            EmployeeDataViewModel model = new EmployeeDataViewModel();
            model.Employees = EmployeeMockData.Employees;

            return View(model);
        }

        //find the employee before you update
        [HttpPost]
        public IActionResult FindEmployee(int employeeId)
        {
            //in order to fix the error message "Converting null literal or possible null value to non-nullable type.",
            //I can add ? to Employee to allow it to be null...but I don';t want it to be null

            //circle back to nullify if needed

            Employee selectedEmployee =
                EmployeeMockData.Employees
                .FirstOrDefault(employee => employee.EmployeeId == employeeId);

            EmployeeDataViewModel model = new EmployeeDataViewModel();
            model.Employees = EmployeeMockData.Employees;

            if (selectedEmployee != null)
            {
                model.SelectedEmployee = selectedEmployee;
                return View("UpdateView", model);
            }

            // not found: stay on UpdateView and show an error message
            model.ErrorMessage = "Employee with ID " + employeeId + " was not found.";
            return View("UpdateView", model);
        }

        //update 
        [HttpPost]
        public IActionResult Update(EmployeeDataViewModel model)
        {
            // Expect the updated employee in model.SelectedEmployee
            var employee = model?.SelectedEmployee;

            if (employee == null)
            {
                return Content("<script>alert('No employee data submitted.'); window.location.href='/Home/Index';</script>", "text/html");
            }

            if (string.IsNullOrWhiteSpace(employee.FirstName) ||
                string.IsNullOrWhiteSpace(employee.LastName) ||
                string.IsNullOrWhiteSpace(employee.Email) ||
                string.IsNullOrWhiteSpace(employee.Department))
            {
                return Content("<script>alert('All fields are required.'); window.location.href='/Home/Index';</script>", "text/html");
            }

            Employee existingEmployee = EmployeeMockData.Employees
                .FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

            if (existingEmployee == null)
            {
                return Content("<script>alert('Employee with ID " + employee.EmployeeId + " was not found.'); window.location.href='/Home/Index';</script>", "text/html");
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Department = employee.Department;

            return Content("<script>alert('Employee successfully updated!'); window.location.href='/Home/Index';</script>", "text/html");
        }
    }
}
