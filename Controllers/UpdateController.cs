using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class UpdateController : Controller
    { 

        private EmployeeDataViewModel model = new EmployeeDataViewModel();

        public UpdateController()
        {
            model.Employees = EmployeeMockData.Employees;

        }
        public IActionResult UpdateView()
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult FindEmployee(int employeeId)
        {
            Employee selectedEmployee = EmployeeMockData.Employees
                .FirstOrDefault(employee => employee.EmployeeId == employeeId);


            if (selectedEmployee != null)
            {
                model.SelectedEmployee = selectedEmployee;
                return View("UpdateView", model);
            }

            model.ErrorMessage = "Employee with ID " + employeeId + " was not found.";
            return View("UpdateView", model);
        }

        [HttpPost]
        public IActionResult Update(EmployeeDataViewModel model)
        {
            Employee employee = model.SelectedEmployee; //get the selected employee from the model

            //if there is no selected employee
            if (employee == null)
            {
                model.ErrorMessage = "No employee data submitted.";
                return View("UpdateView", model);
            }


            //validation check for the selected employee to see if the model is valid, if not, return to the UpdateView with the model and employee list
            if (!TryValidateModel(employee, "SelectedEmployee"))
            {
                model.Employees = EmployeeMockData.Employees;
                return View("UpdateView", model); //circle back, logic?
            }

            Employee existingEmployee = EmployeeMockData.Employees
                .FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);


            if (existingEmployee == null)
            {
                model.ErrorMessage = "Employee with ID " + employee.EmployeeId + " was not found.";
                model.Employees = EmployeeMockData.Employees;

                return View("UpdateView", model);
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Department = employee.Department;

            TempData["SuccessMessage"] = "Employee successfully updated!";

            return RedirectToAction("Employees", "Home");
        }
    }
}
