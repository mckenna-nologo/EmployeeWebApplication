using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApplication.Controllers
{
    public class UpdateController : Controller
    {

        public IActionResult UpdateView()
        {
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

            model.ErrorMessage = "Employee with ID " + employeeId + " was not found.";
            return View("UpdateView", model);
        }

        //update 
        [HttpPost]
        public IActionResult Update(EmployeeDataViewModel model)
        {
            //the updated employee should be in model.SelectedEmployee
            var employee = model?.SelectedEmployee;
            if (employee == null)
            {
                model ??= new EmployeeDataViewModel(); //CIRCLE BACK
                model.ErrorMessage = "No employee data submitted.";
                return View("UpdateView", model);
            }

            // validate the nested SelectedEmployee using data annotations
            // use the same prefix used by the form inputs so validation messages bind to the fields
            if (!TryValidateModel(employee, "SelectedEmployee"))
            {
                // preserve the employee list in the model and return the view so validation messages display
                model.Employees = EmployeeMockData.Employees;
                return View("UpdateView", model);
            }

            Employee existingEmployee = EmployeeMockData.Employees
                .FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

            if (existingEmployee == null)
            {
                model.ErrorMessage = $"Employee with ID {employee.EmployeeId} was not found.";
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
