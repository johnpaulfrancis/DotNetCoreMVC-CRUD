using DotNetCoreMVC_CRUD.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreMVC_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

    }
}
