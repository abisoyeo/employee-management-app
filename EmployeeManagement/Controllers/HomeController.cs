using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment hostEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();

            return View(model);
        }

        public IActionResult Details(int Id)
        {
            // Instantiate HomeDetailsViewModel and store Employee details and PageTitle
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(Id),
                PageTitle = "Employee Details"
            };

            // Pass the ViewModel object to the View() helper method
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (employee.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    employee.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }

            return View();

        }

        public IActionResult Edit()
        {
            return View();
        }


    }
}
