using EmployeeWebApplication.Interfaces;
using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class UpdateController : Controller
    { 
        private EmployeeDataViewModel model = new EmployeeDataViewModel();
        private readonly IEmployeeHelper _employeeHelper;

        public UpdateController(IEmployeeHelper employeeHelper)
        {
            _employeeHelper = employeeHelper;
            model.Employees = _employeeHelper.GetMockData();
        }
        public IActionResult UpdateView()
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult FindEmployee(int employeeId)
        {
            Employee selectedEmployee = _employeeHelper.GetEmployeeById(employeeId);

            if (selectedEmployee != null)
            {
                model.SelectedEmployee = selectedEmployee;
                return View("UpdateView", model);
            }

            TempData["ErrorMessage"] = "Employee with ID " + employeeId + " was not found.";
            return View("UpdateView", model);
        }

        [HttpPost]
        public IActionResult Update(EmployeeDataViewModel model)
        {
            Employee employee = model.SelectedEmployee; //get the selected employee from the model

            //if there is no selected employee
            if (employee == null)
            {
                TempData["ErrorMessage"] = "No employee data submitted.";
                return View("UpdateView", model);
            }

            //validation check for the selected employee to see if the model is valid, if not, return to the UpdateView with the model and employee list
            if (!TryValidateModel(employee, "SelectedEmployee"))
            {
                model.Employees = EmployeeMockData.Employees;
                return View("UpdateView", model); 
            }

            Employee existingEmployee = _employeeHelper.GetEmployeeById(employee.EmployeeId); 


            if (existingEmployee == null)
            {
                TempData["ErrorMessage"] = "Employee with ID " + employee.EmployeeId + " was not found.";
                model.Employees = EmployeeMockData.Employees;

                return View("UpdateView", model);
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Department = employee.Department;
            existingEmployee.IsActive = employee.IsActive;

            TempData["SuccessMessage"] = "Employee successfully updated!";

            return RedirectToAction("Employees", "Home");
        }
    }
}
