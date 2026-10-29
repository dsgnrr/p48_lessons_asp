using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Models;
using p48_lessons_asp.Services.Operation;

namespace p48_lessons_asp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TransientOperation _transient1;

        private readonly TransientOperation _transient2;

        private readonly ScopedOperation _scoped1;
        private readonly ScopedOperation _scoped2;

        private readonly SingletonOperation _singleton1;
        private readonly SingletonOperation _singleton2;

        public HomeController(
            TransientOperation transient1,
            TransientOperation transient2,
            ScopedOperation scoped1,
            ScopedOperation scoped2,
            SingletonOperation singleton1,
            SingletonOperation singleton2
        )
        {
            _transient1 = transient1;
            _transient2 = transient2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _singleton1 = singleton1;
            _singleton2 = singleton2;
        }
        public IActionResult UserPage()
        {
            return View("Index");
        }

        public IActionResult Service()
        {
            ViewBag.Transient1 = _transient1.OperationId;
            ViewBag.Transient2 = _transient2.OperationId;

            ViewBag.Scoped1 = _scoped1.OperationId;
            ViewBag.Scoped2 = _scoped2.OperationId;

            ViewBag.Singleton1 = _singleton1.OperationId;
            ViewBag.Singleton2 = _singleton2.OperationId;
            return View();
        }

        // action
        public IActionResult Index(int? id)
        {

            // Словник, який можна використовувати для передачі інформації на сторінку, має синтаксис та методи звичайного словника
            ViewData["Message"] = "Welcome to ASP.NET Core MVC";
            // Синтаксичний цукор для ViewData може містити dynamic props що не потребує приводити тип
            ViewBag.User = new
            {
                Name = "Petro",
                Surname = "Ivanov",
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