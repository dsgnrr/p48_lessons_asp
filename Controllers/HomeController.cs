using Microsoft.AspNetCore.Mvc;

namespace p48_lessons_asp.Controllers
{
    public class HomeController : Controller
    {
        // action
        public IActionResult Index() => View();
    }
}