using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeWebApplication.Controllers
{
    public class HomeController : Controller
    {
        //read
        public IActionResult Index()
        {
            EmployeeDataViewModel model = new EmployeeDataViewModel();

            model.Employees = EmployeeMockData.Employees;

            //return View();
            return View(model);
        }

        public IActionResult Employees()
        {
            EmployeeDataViewModel model = new EmployeeDataViewModel();
            model.Employees = EmployeeMockData.Employees;

            return View(model);
            //return View();
        }


        public IActionResult SearchView()
        {
            // Ensure the SearchView always receives a non-null model to avoid null reference in the Razor page
            EmployeeDataViewModel model = new EmployeeDataViewModel();
            model.Employees = EmployeeMockData.Employees;
            model.HasSearched = false;

            return View(model);
        }

        //search through employee data
        [HttpPost]
        public IActionResult Search(string name, string department)
        {
            List<Employee> searchResults = EmployeeMockData.Employees
                .Where(employee => //where a particular employee
                (string.IsNullOrEmpty(name) || employee.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase)) &&  //the name is either null and the department is entered
                (string.IsNullOrEmpty(department) || employee.Department.Contains(department, StringComparison.OrdinalIgnoreCase)) //or the department is null and the name is entered
                )
                .ToList(); //put the results in a list 


            EmployeeDataViewModel model = new EmployeeDataViewModel(); 

            model.Employees = EmployeeMockData.Employees; //not working
            model.SearchResults = searchResults;
            model.HasSearched = true;

            // return SearchView with search results
            return View("SearchView", model);
        }


        ////find the employee before you update
        //[HttpPost]
        //public IActionResult FindEmployee(int employeeId)
        //{
        //    //in order to fix the error message "Converting null literal or possible null value to non-nullable type.",
        //    //I can add ? to Employee to allow it to be null...but I don';t want it to be null

        //    //circle back to nullify if needed

        //    Employee selectedEmployee =
        //        EmployeeMockData.Employees
        //        .FirstOrDefault(employee => employee.EmployeeId == employeeId);

        //    EmployeeDataViewModel model = new EmployeeDataViewModel();
        //    model.Employees = EmployeeMockData.Employees;

        //    if (selectedEmployee != null)
        //    {
        //        model.SelectedEmployee = selectedEmployee;
        //        return View("UpdateView", model);
        //    }

        //    // not found: stay on UpdateView and show an error message
        //    model.ErrorMessage = "Employee with ID " + employeeId + " was not found.";
        //    return View("UpdateView", model);
        //}

        //public IActionResult UpdateView()
        //{
        //    // Ensure the UpdateView always receives a non-null model to avoid null reference in the Razor page
        //    EmployeeDataViewModel model = new EmployeeDataViewModel();
        //    model.Employees = EmployeeMockData.Employees;

        //    return View(model);
        //}

        ////update the
        //[HttpPost]
        //public IActionResult Update(EmployeeDataViewModel model)
        //{
        //    // Expect the updated employee in model.SelectedEmployee
        //    var employee = model?.SelectedEmployee;

        //    if (employee == null)
        //    {
        //        return Content("<script>alert('No employee data submitted.'); window.location.href='/Home/Index';</script>", "text/html");
        //    }

        //    if (string.IsNullOrWhiteSpace(employee.FirstName) ||
        //        string.IsNullOrWhiteSpace(employee.LastName) ||
        //        string.IsNullOrWhiteSpace(employee.Email) ||
        //        string.IsNullOrWhiteSpace(employee.Department))
        //    {
        //        return Content("<script>alert('All fields are required.'); window.location.href='/Home/Index';</script>", "text/html");
        //    }

        //    Employee existingEmployee = EmployeeMockData.Employees
        //        .FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

        //    if (existingEmployee == null)
        //    {
        //        return Content("<script>alert('Employee with ID " + employee.EmployeeId + " was not found.'); window.location.href='/Home/Index';</script>", "text/html");
        //    }

        //    existingEmployee.FirstName = employee.FirstName;
        //    existingEmployee.LastName = employee.LastName;
        //    existingEmployee.Email = employee.Email;
        //    existingEmployee.Department = employee.Department;

        //    return Content("<script>alert('Employee successfully updated!'); window.location.href='/Home/Index';</script>", "text/html");
        //}

        public IActionResult DeleteView()
        {
            // Ensure the DeleteView always receives a non-null model to avoid null reference in the Razor page
            EmployeeDataViewModel model = new EmployeeDataViewModel();
            model.Employees = EmployeeMockData.Employees;
            return View(model);
        }

        //delete
        [HttpPost]
        public IActionResult Delete(int employeeId)
        {
            Employee employee = EmployeeMockData.Employees
                .FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee != null)
            {
                EmployeeMockData.Employees.Remove(employee);
            }

            return RedirectToAction("Index");
        }

        public IActionResult CsvView()
        {
            return View();
        }

        //export to csv
        public IActionResult ExportCsv() 
        {
            StringBuilder csv = new StringBuilder(); 

            csv.AppendLine("Employee ID,First Name,Last Name,Email,Department,Date Created"); 

            EmployeeMockData.Employees 
                .Select(employee =>
                    employee.EmployeeId + "," +
                    EscapeCsv(employee.FirstName) + "," +
                    EscapeCsv(employee.LastName) + "," +
                    EscapeCsv(employee.Email) + "," +
                    EscapeCsv(employee.Department) + "," +
                    employee.DateCreated.ToString("yyyy-MM-dd")
                )
                .ToList()
                .ForEach(line => csv.AppendLine(line)); 

            byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString()); 

            return File(bytes, "text/csv", "Employees.csv"); 
        }

        private string EscapeCsv(string value) 
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n")) //checks if values contain a comma quote or new line
            {
                value = value.Replace("\"", "\"\""); //adds quotes 
                return "\"" + value + "\""; //put the whole value inside quotes

                //use a backslash to tell the compiler that the quote is literal text
                //two literal double quote characters used as the replacement
                //referenceed from https: //learn.microsoft.com/en-us/dotnet/api/system.string.replace?view=net-10.0
            }

            return value;
        }

        //import csv
        [HttpPost] 
        public IActionResult ImportCsv(IFormFile file) 
        {
            if (file == null)
            {
                return Content("<script>alert('Please select a CSV file.'); window.location.href='/Home/Index';</script>", "text/html");
            } 
            else if (file.Length == 0)
            {
                return Content("<script>alert('This CSV is empty.'); window.location.href='/Home/Index';</script>", "text/html");
            }

            using (StreamReader reader = new StreamReader(file.OpenReadStream())) //referenced https: //www.geeksforgeeks.org/c-sharp/streamreader-and-streamwriter-in-c-sharp/
            {
                string line;
                reader.ReadLine();

                while ((line = reader.ReadLine()) != null) {
                    
                    List<string> data = new List<string>();

                    //string removeHeadings = line.Replace("Employee ID,First Name,Last Name,Email,Department,Date Created", ""); //not working

                    data = line.Split(",").ToList();

                    Employee employee = new Employee();

                    employee.EmployeeId = EmployeeMockData.GetNextId();
                    employee.FirstName = data[1];
                    employee.LastName = data[2];
                    employee.Email = data[3];
                    employee.Department = data[4];
                    employee.DateCreated = DateTime.Parse(data[5]);  //not working
                    //employee.DateCreated = DateTime.Now;

                    EmployeeMockData.Employees.Add(employee);
                }
            }

            return Content("<script>alert('Employee Data successfully updated!'); window.location.href = '/Home/Index';</script>", "text/html");
        }
    }
}