using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //=stack overflow. 

namespace EmployeeWebApplication.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        //First Name
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        //Last Name
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        //Email
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Pleease enter a valid email address")]
        public string Email { get; set; } = string.Empty;

        //Department
        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } //set in contoller

    }
}
