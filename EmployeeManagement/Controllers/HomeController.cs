using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
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

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            Employee employee = _employeeRepository.GetEmployee(id.Value);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            // Instantiate HomeDetailsViewModel and store Employee details and PageTitle
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(employeeModel.Photo);

                Employee newEmployee = new Employee
                {
                    Name = employeeModel.Name,
                    Email = employeeModel.Email,
                    Department = employeeModel.Department,
                    PhotoPath = uniqueFileName // Store the unique file name in the database
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }

            return View();

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);

            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                Employee modifiedEmployee = _employeeRepository.GetEmployee(employeeModel.Id);
                modifiedEmployee.Name = employeeModel.Name;
                modifiedEmployee.Email = employeeModel.Email;
                modifiedEmployee.Department = employeeModel.Department;

                // Check if there is an existing photo that needs to be deleted
                if (!string.IsNullOrEmpty(employeeModel.ExistingPhotoPath))
                {
                    // Combine the path to get the full file path of the existing photo
                    string filePath = Path.Combine(hostEnvironment.WebRootPath,
                            "images", employeeModel.ExistingPhotoPath);
                    // Delete the existing photo from the file system
                    System.IO.File.Delete(filePath); 
                }

                // Process the uploaded file and get the new photo path
                modifiedEmployee.PhotoPath = ProcessUploadedFile(employeeModel.Photo);

                _employeeRepository.Update(modifiedEmployee);
                return RedirectToAction("Index");
            }

            return View();
        }

        private string ProcessUploadedFile(IFormFile photo)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                // Get the path to the wwwroot/images directory
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                // Create a unique file name for the uploaded photo to avoid overwriting files with the same name
                // and makes it easier to reference the file later
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Save the uploaded photo to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
