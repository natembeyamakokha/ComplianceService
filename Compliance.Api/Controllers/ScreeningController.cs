using Microsoft.AspNetCore.Mvc;

namespace Compliance.Api.Controllers
{
    public class ScreeningController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
