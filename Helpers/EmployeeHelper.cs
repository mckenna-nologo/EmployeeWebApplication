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

        public void Delete(Employee employee) 
        {
            EmployeeMockData.Employees.Remove(employee);
          
        }
        public List<Employee> GetMockData()
        {
            return EmployeeMockData.Employees;
        }

        public void Search(string name, string department)
        {
            //what goes here?
        }

        public void Update(Employee model)
        {
             
        }

        public Employee GetEmployeeById (int employeeId)
        {
            return EmployeeMockData.Employees.FirstOrDefault(employee => employee.EmployeeId == employeeId);
        }
    }
}
