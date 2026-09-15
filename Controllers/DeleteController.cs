using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class DeleteController : Controller
    {
        public IActionResult DeleteView()
        {
            // Ensure the DeleteView always receives a non-null model to avoid null reference in the Razor page
            EmployeeDataViewModel model = new EmployeeDataViewModel();
            model.Employees = EmployeeMockData.Employees;
            return View(model);
        }


        //find the employee before you update
        [HttpPost]
        public IActionResult FindEmployee(int employeeId)
        {
            Employee selectedEmployee =
                EmployeeMockData.Employees
                .FirstOrDefault(employee => employee.EmployeeId == employeeId);

            EmployeeDataViewModel model = new EmployeeDataViewModel();
            model.Employees = EmployeeMockData.Employees;

            if (selectedEmployee != null)
            {
                model.SelectedEmployee = selectedEmployee;
                return View("DeleteView", model);
            }

            model.ErrorMessage = "Employee with ID " + employeeId + " was not found.";
            return View("DeleteView", model);
        }


        //delete
        [HttpPost]
        public IActionResult Delete(EmployeeDataViewModel model)
        {
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

            if (employee != null)
            {
                EmployeeMockData.Employees.Remove(employee);
            }

            if (existingEmployee == null)
            {
                return Content("<script>alert('Employee with ID " + employee.EmployeeId + " was not found.'); window.location.href='/Home/Index';</script>", "text/html");
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Department = employee.Department;

            // After deletion, redirect back to the Home Index (not the Delete controller's Index
            // which doesn't exist). This avoids the 404 at /Delete.
            return RedirectToAction("Index", "Home");
        }
    }
}
