using DotNetCoreMVC_CRUD.Data;
using DotNetCoreMVC_CRUD.Models;
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
                DateOfBirth =e.DateOfBirth,
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
    }
}
