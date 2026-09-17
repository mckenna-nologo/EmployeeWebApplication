using EmployeeWebApplication.Models;

namespace EmployeeWebApplication.Interfaces
{
    public interface IEmployeeHelper
    {
        void Create(Employee employee);

        void Delete(Employee employee);

        List<Employee> GetMockData();

        void Search(string name, string department); //circle back

        void Update(Employee model); //circle back

        Employee GetEmployeeById(int employeeId);

    }
}
