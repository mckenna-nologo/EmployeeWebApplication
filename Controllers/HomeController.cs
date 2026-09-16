using EmployeeWebApplication.Mock;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeWebApplication.Controllers
{
    public class HomeController : Controller
    {
        //solid + interfaces

        private EmployeeDataViewModel model = new EmployeeDataViewModel(); //fields 

        public HomeController() //constructor
        {
            model.Employees = EmployeeMockData.Employees;
        }

        public IActionResult Index()
        {
            return View(model);
        }

        public IActionResult Employees()
        {
            return View(model);
        }
    }
}