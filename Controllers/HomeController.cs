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
    }
}