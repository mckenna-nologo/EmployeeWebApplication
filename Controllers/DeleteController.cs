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


        // find the employee before you delete
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int employeeId)
        {
            var employee = EmployeeMockData.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                TempData["DeleteError"] = $"Employee with ID {employeeId} was not found.";
                return RedirectToAction("DeleteView");
            }

            EmployeeMockData.Employees.Remove(employee);

            return Content("<script>alert('Employee successfully deleted!'); window.location.href='/Home/Index';</script>", "text/html");
            
        }
    }
}
