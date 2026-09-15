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

        //delete
        [HttpPost]
        public IActionResult Delete(int employeeId)
        {
            Employee employee = EmployeeMockData.Employees
                .FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee != null)
            {
                EmployeeMockData.Employees.Remove(employee);
            }

            // After deletion, redirect back to the Home Index (not the Delete controller's Index
            // which doesn't exist). This avoids the 404 at /Delete.
            return RedirectToAction("Index", "Home");
        }
    }
}
