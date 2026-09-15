using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EmployeeWebApplication.Controllers
{
    public class CsvController : Controller
    {
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
                TempData["ErrorMessage"] = "Please select a CSV file.";
                //return RedirectToAction("Index", "Home");
            }
            else if (file.Length == 0)
            {
                TempData["ErrorMessage"] = "This CSV is empty.";
                //return RedirectToAction("Index", "Home");
            }

            using (StreamReader reader = new StreamReader(file.OpenReadStream())) //referenced https: //www.geeksforgeeks.org/c-sharp/streamreader-and-streamwriter-in-c-sharp/
            {
                string line;
                reader.ReadLine();

                while ((line = reader.ReadLine()) != null)
                {

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

            TempData["SuccessMessage"] = "Employee data successfully imported!";
            return RedirectToAction("Employees", "Home");
        }
    }
}
