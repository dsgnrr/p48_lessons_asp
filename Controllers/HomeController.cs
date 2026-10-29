using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Models;
using p48_lessons_asp.Services;
using p48_lessons_asp.Services.StorageService;

namespace p48_lessons_asp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly S3Service _s3Service;

        private readonly IStorageService _storageService;

        public HomeController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        //public HomeController()
        //{
        //    //_s3Service = new("ey54235235235");
        //}

        public IActionResult UserPage()
        {
            return View("Index");
        }

        // action
        public IActionResult Index(int? id)
        {
            _storageService.LoadFile("Filename");

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