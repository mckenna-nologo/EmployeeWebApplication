using EmployeeWebApplication.Interfaces;
using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;

namespace EmployeeWebApplication.Helpers
{
    public class EmployeeHelper : IEmployeeHelper
    {

        public void Create(Employee employee) 
        { 
            employee.EmployeeId = EmployeeMockData.GetNextId();
            employee.DateCreated = DateTime.Now;

             EmployeeMockData.Employees.Add(employee);
        }
    }
}
