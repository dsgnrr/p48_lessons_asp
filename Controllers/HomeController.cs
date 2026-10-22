using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Models;

namespace p48_lessons_asp.Controllers
{
    public class HomeController : Controller
    {
        // action
        public IActionResult Index(int? id)
        {
            // Словник, який можна використовувати для передачі інформації на сторінку, має синтаксис та методи звичайного словника
            ViewData["Message"] = "Welcome to ASP.NET Core MVC";
            // Синтаксичний цукор для ViewData може містити dynamic props що не потребує приводити тип
            ViewBag.User = new
            {
                Name="Petro",
                Surname="Ivanov",
                Age = 20
            };
            if (id.HasValue)
            {
                System.Console.WriteLine($"ID: {id.Value}");
            }
            return View();
        }

        public IActionResult Product()
        {
            var product = new ProductModel
            {
                Name = "LG Monitor",
                Price = 100
            };
            return View(product);
        }

        public IActionResult SecondPage() => View();

        public IActionResult Save()
        {
            TempData["TempMessage"] = "This a temporary message";
            return RedirectToAction("Index");
        }
    }
}