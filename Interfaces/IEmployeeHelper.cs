using EmployeeWebApplication.Models;

namespace EmployeeWebApplication.Interfaces
{
    public interface IEmployeeHelper
    {
        void Create(Employee employee);

        void Delete(Employee employee);
    }
}
