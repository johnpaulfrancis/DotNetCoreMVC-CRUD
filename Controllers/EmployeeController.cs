using DotNetCoreMVC_CRUD.Data;
using DotNetCoreMVC_CRUD.HelperClass;
using DotNetCoreMVC_CRUD.Models;
using DotNetCoreMVC_CRUD.Models.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace DotNetCoreMVC_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly EncryptDecrypt _encryptDecrypt;

        public EmployeeController(AppDbContext appDbContext, EncryptDecrypt encryptDecrypt)
        {
            _appDbContext = appDbContext;
            _encryptDecrypt = encryptDecrypt;
        }

        // GET: Employee
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _appDbContext.EmployeeMaster.ToListAsync();
            List<EmployeeMasterModel> empModel = employees.Select(e => new EmployeeMasterModel
            {
                EmployeeId = e.EmployeeId,
                EncryptedEmpId = _encryptDecrypt.Encrypt(Convert.ToString(e.EmployeeId)), // Encrypting EmployeeId for security
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
                    EmployeeName = employeeModel.EmployeeName,
                    Email = employeeModel.Email,
                    Gender = employeeModel.Gender,
                    DateOfBirth = employeeModel.DateOfBirth,
                };

                // Decrypting EmployeeId
                if (!string.IsNullOrEmpty(employeeModel.EncryptedEmpId)
                    && int.TryParse(_encryptDecrypt.Decrypt(HttpUtility.UrlDecode(employeeModel.EncryptedEmpId).Replace(" ", "+")), out int empId) && empId > 0)
                {
                    employee.EmployeeId = empId; 
                }

                if (employee.EmployeeId > 0)
                {
                    employee.UpdatedAt = DateTime.Now;
                    _appDbContext.EmployeeMaster.Update(employee); //Update the employee to the database context
                }
                else
                {
                    employee.CreatedAt = DateTime.Now;
                    _appDbContext.EmployeeMaster.Add(employee); // Add the new employee to the database context
                }
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index"); // Redirect to the Index action after successful creation
            }
            return View(employeeModel); // If the model state is invalid, return the same view with the model to show validation errors
        }

        // POST: DeleteEmployee

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(string empId)
        {
            empId = HttpUtility.UrlDecode(empId);
            empId = empId.Replace(" ", "+");
            int id = int.Parse(_encryptDecrypt.Decrypt(empId));
            if (id <= 0)
            {
                return NotFound(); // If empId is invalid, return NotFound
            }
            var employee = await _appDbContext.EmployeeMaster.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _appDbContext.EmployeeMaster.Remove(employee); // Remove the employee from the database context
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index"); // Redirect to the Index action after successful deletion
        }

        // GET: Edit Employee
        //parameter should be what we given after "asp-route-" in the anchor tag
        [HttpGet]
        public async Task<IActionResult> EditEmployee(string id)
        {
            id = HttpUtility.UrlDecode(id);
            id = id.Replace(" ", "+");
            if (int.TryParse(_encryptDecrypt.Decrypt(id), out int empId))
            {
                var emp = await _appDbContext.EmployeeMaster.FindAsync(empId);
                if (emp == null)
                {
                    return NotFound();
                }
                var empModel = new EmployeeMasterModel
                {
                    EmployeeId = emp.EmployeeId,
                    EmployeeName = emp.EmployeeName,
                    Gender = emp.Gender,
                    DateOfBirth = emp.DateOfBirth,
                    Email = emp.Email,
                    EncryptedEmpId = id
                };
                return View("CreateEmployee", empModel);
            }
            return View();
        }
    }
}
