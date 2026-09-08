using EmployeeWebApplication.Models;

namespace EmployeeWebApplication.Mock
{
    public static class EmployeeMockData
    {
        public static List<Employee> Employees { get; } =
        [
            new Employee
            {
                EmployeeId = 1,
                FirstName = "Emma",
                LastName = "Stone",
                Email = "emmastone@gmail.com",
                Department = "Finance",
                DateCreated = new DateTime(2026, 1, 15)
            },
            new Employee
            {
                EmployeeId = 2,
                FirstName = "Henry",
                LastName = "Danger",
                Email = "henrydanger@gmail.com",
                Department = "IT",
                DateCreated = new DateTime(2026, 1, 16)
            },
            new Employee
            {
                EmployeeId = 3,
                FirstName = "Bill",
                LastName = "Cosby",
                Email = "billcosby@gmail.com",
                Department = "Culinary",
                DateCreated = new DateTime(2026, 1, 17)
            },
            new Employee
            {
                EmployeeId = 4,
                FirstName = "Joan",
                LastName = "Jett",
                Email = "joanjett@gmail.com",
                Department = "Music",
                DateCreated = new DateTime(2026, 1, 18)
            },
            new Employee
            {
                EmployeeId = 5,
                FirstName = "Axl",
                LastName = "Rose",
                Email = "axlrose@gmail.com",
                Department = "Music",
                DateCreated = new DateTime(2026, 1, 18)
            },
            new Employee
            {
                EmployeeId = 6,
                FirstName = "Ugly",
                LastName = "Betty",
                Email = "uglybetty.com",
                Department = "Hollywood",
                DateCreated = new DateTime(2026, 3, 7)
            },
            new Employee
            {
                EmployeeId = 7,
                FirstName = "Jack",
                LastName = "Jill",
                Email = "jackjill@gmail.com",
                Department = "Agriculture",
                DateCreated = new DateTime(2026, 3, 17)
            },
            new Employee
            {
                EmployeeId = 8,
                FirstName = "Joan",
                LastName = "Jillian",
                Email = "joanjillian@gmail.com",
                Department = "Music",
                DateCreated = new DateTime(2026, 4, 9)
            },
            new Employee
            {
                EmployeeId = 9,
                FirstName = "Happy",
                LastName = "Go'Lucky",
                Email = "happygolucky@gmail.com",
                Department = "Agriculture",
                DateCreated = new DateTime(2026, 4, 8)
            },
        ];

        public static int GetNextId()
        {
            if (Employees.Count == 0)
            {
                return 1;
            }

            return Employees.Max(employee => employee.EmployeeId) + 1;
        }
    }
}
