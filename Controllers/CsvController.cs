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
            return View(); //show the page
        }

        private List<string> ParseCsvLine(string line) //parse a single line of CSV into a list of strings, handling quoted fields and commas
        {
            var result = new List<string>(); 
            if (line == null) return result; 

            var sb = new System.Text.StringBuilder(); 
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++) //loop through eauch character in the line
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"');
                        i++; //skip next quote
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }

            result.Add(sb.ToString());
            //trim quotes and whitespace
            for (int i = 0; i < result.Count; i++)
            {
                var s = result[i].Trim();
                if (s.Length >= 2 && s.StartsWith("\"") && s.EndsWith("\""))
                {
                    s = s.Substring(1, s.Length - 2).Replace("\"\"", "\"");
                }
                result[i] = s;
            }

            return result;
        }

        private void ProcessLine(string line, ref int added, ref int skippedDuplicates, ref int skippedInvalid, int lineNumber)
        {
            var data = ParseCsvLine(line);

            string first = string.Empty; //circle back --> repetition
            string last = string.Empty;
            string email = string.Empty;
            string department = string.Empty;
            string dateStr = string.Empty;
            bool isActive = true;

            // Support multiple CSV formats:
            // 1) ID,First,Last,Email,Department,Date (exported by app)
            // 2) ID,First,Last,Email,Status,Department,Date (user CSV with Status column)
            // 3) First,Last,Email,Department (minimal)
            if (data.Count >= 7)
            {
                // assume: id, first, last, email, status, department, date
                first = data[1]?.Trim() ?? string.Empty;
                last = data[2]?.Trim() ?? string.Empty;
                email = data[3]?.Trim() ?? string.Empty;
                var status = data[4]?.Trim() ?? string.Empty;
                department = data[5]?.Trim() ?? string.Empty;
                dateStr = data[6]?.Trim() ?? string.Empty;

                if (!string.IsNullOrEmpty(status))
                {
                    isActive = status.Equals("active", StringComparison.OrdinalIgnoreCase);
                }
            }
            else if (data.Count >= 6)
            {
                // assume: id, first, last, email, department, date
                first = data[1]?.Trim() ?? string.Empty;
                last = data[2]?.Trim() ?? string.Empty;
                email = data[3]?.Trim() ?? string.Empty;
                department = data[4]?.Trim() ?? string.Empty;
                dateStr = data[5]?.Trim() ?? string.Empty;
            }
            else if (data.Count >= 4)
            {
                // assume: first, last, email, department
                first = data[0]?.Trim() ?? string.Empty;
                last = data[1]?.Trim() ?? string.Empty;
                email = data[2]?.Trim() ?? string.Empty;
                department = data[3]?.Trim() ?? string.Empty;
            }
            else
            {
                skippedInvalid++;
                return;
            }

            //validation
            if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(department))
            {
                skippedInvalid++;
                return;
            }

            //check duplicates by email or exact first+last
            var exists = EmployeeMockData.Employees.Any(e =>
                (!string.IsNullOrEmpty(email) && e.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) ||
                (e.FirstName.Equals(first, StringComparison.OrdinalIgnoreCase) && e.LastName.Equals(last, StringComparison.OrdinalIgnoreCase)));

            if (exists)
            {
                skippedDuplicates++;
                return;
            }

            var employee = new Employee();
            employee.EmployeeId = EmployeeMockData.GetNextId();
            employee.FirstName = first;
            employee.LastName = last;
            employee.Email = email;
            employee.Department = department;
            employee.IsActive = isActive;

            if (!string.IsNullOrWhiteSpace(dateStr) && DateTime.TryParse(dateStr, out var dt))
            {
                employee.DateCreated = dt;
            }
            else
            {
                employee.DateCreated = DateTime.Now;
            }

            EmployeeMockData.Employees.Add(employee);
            added++;
        }

        //export to csv
        public IActionResult ExportCsv()
        {
            StringBuilder csv = new StringBuilder();

            csv.AppendLine("Employee ID,First Name,Last Name,Email,Status,Department,Date Created");

            EmployeeMockData.Employees
                .Select(employee =>
                    employee.EmployeeId + "," +
                    EscapeCsv(employee.FirstName) + "," +
                    EscapeCsv(employee.LastName) + "," +
                    EscapeCsv(employee.Email) + "," +
                    EscapeCsv(employee.IsActive ? "Active" : "Inactive") + "," +
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
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = file == null ? "Please select a CSV file." : "This CSV is empty.";
                return RedirectToAction("CsvView");
            }

            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "Please upload a .csv file.";
                return RedirectToAction("CsvView");
            }

            int added = 0;
            int skippedDuplicates = 0;
            int skippedInvalid = 0;
            var lineNumber = 0;

            try
            {
                using (StreamReader reader = new StreamReader(file.OpenReadStream()))
                {
                    string firstLine = reader.ReadLine();
                    lineNumber++;

                    bool headerDetected = false;
                    if (!string.IsNullOrWhiteSpace(firstLine))
                    {
                        var low = firstLine.ToLowerInvariant();
                        if (low.Contains("first") || low.Contains("employee") || low.Contains("email") || low.Contains("last") || low.Contains("department"))
                        {
                            headerDetected = true;
                        }
                    }

                    //if first line wasn't a header, process it as data
                    if (!headerDetected && !string.IsNullOrWhiteSpace(firstLine))
                    {
                        ProcessLine(firstLine, ref added, ref skippedDuplicates, ref skippedInvalid, lineNumber);
                    }

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        ProcessLine(line, ref added, ref skippedDuplicates, ref skippedInvalid, lineNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Failed to import CSV: " + ex.Message;
                return RedirectToAction("CsvView");
            }

            if (added > 0)
            {
                TempData["SuccessMessage"] = $"Imported {added} employees. {skippedDuplicates} duplicates skipped, {skippedInvalid} invalid rows skipped.";
            }
            else
            {
                TempData["ErrorMessage"] = $"No employees were imported. {skippedDuplicates} duplicates skipped, {skippedInvalid} invalid rows skipped.";
            }

            return RedirectToAction("Employees", "Home");
        }
    }
}
