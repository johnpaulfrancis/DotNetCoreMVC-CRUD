using DotNetCoreMVC_CRUD.Data;
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
            return View(employees);
        }
    }
}
