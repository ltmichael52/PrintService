using Microsoft.AspNetCore.Mvc;

namespace PrintService.Controllers
{
    public class UserInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
