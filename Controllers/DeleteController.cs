using EmployeeWebApplication.Interfaces;
using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class DeleteController : Controller
    {
        private EmployeeDataViewModel model = new EmployeeDataViewModel();
        private readonly IEmployeeHelper _employeeHelper;

        public DeleteController(IEmployeeHelper employeeHelper)
        {
            _employeeHelper = employeeHelper;

            model.Employees = _employeeHelper.GetMockData(); //circle back
        }

        public IActionResult DeleteView()
        {
            return View(model);
        }


        // find the employee before you delete
        [HttpPost]
        public IActionResult FindEmployee(int employeeId)
        {
            var selectedEmployee = _employeeHelper.GetEmployeeById(employeeId);

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
        public IActionResult Delete(int employeeId)
        {
            var employee = _employeeHelper.GetEmployeeById(employeeId);

            if (employee == null)
            {
                TempData["ErrorMessage"] = $"Employee with ID {employeeId} was not found.";
                return RedirectToAction("DeleteView");
            }

            _employeeHelper.Delete(employee);

            TempData["SuccessMessage"] = "Employee successfully deleted!";
            return RedirectToAction("Employees", "Home");
            
        }
    }
}
