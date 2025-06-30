using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreMVC_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
