using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
    }
}
