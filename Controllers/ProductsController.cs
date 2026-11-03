using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Models;
using p48_lessons_asp.Services.Generator;

namespace p48_lessons_asp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IGenerator<Product> _generator;
        private readonly List<Product> products;
        public ProductsController(IGenerator<Product> generator)
        {
            _generator = generator;
            products = _generator.Generate(100);

        }

        [HttpGet]
        public IActionResult Products()
        {
            return Ok(new
            {
                Status = 200,
                Message = "Ok",
                Products = products
            });
        }
        [HttpGet]
        [Route("query")]
        public IActionResult GetProductsIn([FromQuery] int? startFrom, [FromQuery] int? endsWith)
        {
            if (startFrom.HasValue)
            {
                if (startFrom.Value < 0 || startFrom.Value > products.Count - 1)
                {
                    return BadRequest(new
                    {
                        Status = 400,
                        Message = $"Start from must be in 0-{products.Count}"
                    });
                }
                return Ok(products.Slice(startFrom.Value, (products.Count - 1) - startFrom.Value));
            }
            if (endsWith.HasValue)
            {
                if (endsWith < products.Count || endsWith > products.Count - 1)
                {
                    return BadRequest(new
                    {
                        Status = 400,
                        Message = $"endsWith must be in 0-{products.Count - 1}"
                    });
                }
            }
            return NotFound();
        }

        //Реалізація рут параметру
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            if (id > (products.Count - 1)) return NotFound(new
            {
                Status = 404,
                Message = $"Product with id-{id} not found",
                Data = (object)null!
            });
            // return Ok(products[id]);
            return Ok(new
            {
                Status = 200,
                Message = "Ok",
                Data = products[id]
            });
        }
        // POST: api/products/
        [HttpPost]
        public IActionResult CreateProducts([FromBody]CreateProductModel model)
        {
            System.Console.WriteLine(model.Product);
            return Ok();
        }
    }
}
