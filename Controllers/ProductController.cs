using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Models;

namespace p48_lessons_asp.Controllers
{
    public class ProductController : Controller
    {
        // DbContext
        private static List<ProductListItemModel> _products = new()
        {
            new() {Name="Macbook Pro M4", Price = 90000, CategoryName="Most expensive gadgets(Apple)"},
            new() {Name="Samsung Washmachine", Price = 30000, CategoryName = "Appliances"}
        };

        private static Dictionary<Guid, string> _categories = new (){
            {Guid.Parse("ef2e3af3-ee40-4351-be38-3231b1505a96"), "Most expensive gadgets(Apple)"},
            {Guid.Parse("11ba17ad-3ef4-49b2-8a24-194ac2db596f"), "Appliances"},
            {Guid.Parse("afe51c95-3926-40c8-8fb3-3374c447ade6"),"Clothes"}
        };

        public ActionResult Index()
        {
            return View(_products);
        }

    }
}
