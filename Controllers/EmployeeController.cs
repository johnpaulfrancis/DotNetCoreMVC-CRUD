using DotNetCoreMVC_CRUD.Data;
using DotNetCoreMVC_CRUD.Models;
using DotNetCoreMVC_CRUD.Models.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreMVC_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: Employee
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _appDbContext.EmployeeMaster.ToListAsync();
            List<EmployeeMasterModel> empModel = employees.Select(e => new EmployeeMasterModel
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                Email = e.Email,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            }).ToList(); // Mapping to view model from db schema model class
            return View(empModel);
        }

        //GET: Employee/Create
        // This action method is used to display the form for creating a new employee
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        // POST: Employee/Create
        // This action method is used to handle the form submission for creating a new employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(EmployeeMasterModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the database schema model
                var employee = new EmployeeMaster_Schema
                {
                    EmployeeId = (employeeModel.EmployeeId != null) ? (int)employeeModel.EmployeeId : 0,
                    EmployeeName = employeeModel.EmployeeName,
                    Email = employeeModel.Email,
                    Gender = employeeModel.Gender,
                    DateOfBirth = employeeModel.DateOfBirth,
                    CreatedAt = DateTime.Now
                };
                _appDbContext.EmployeeMaster.Add(employee); // Add the new employee to the database context
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index"); // Redirect to the Index action after successful creation
            }
            return View(employeeModel); // If the model state is invalid, return the same view with the model to show validation errors
        }
    }
}
