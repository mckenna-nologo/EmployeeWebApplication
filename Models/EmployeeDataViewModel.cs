namespace EmployeeWebApplication.Models
{
    public class EmployeeDataViewModel
    {
        public List<Employee> ?Employees { get; set; } //the employee list

        public Employee ?SelectedEmployee { get; set; } //if we find an employee for the update function

        public string ErrorMessage { get; set; } = string.Empty; //if the id isnt found

        public List<Employee> SearchResults { get; set; } = new List<Employee>(); //for searching

        public bool HasSearched { get; set; }

    }
}
