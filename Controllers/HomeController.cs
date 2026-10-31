using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Models;
using p48_lessons_asp.Services.Generator;
using Swashbuckle.AspNetCore.Annotations;

namespace p48_lessons_asp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IGenerator<Product> _generator;
        private readonly List<Product> products;
        public HomeController(IGenerator<Product> generator)
        {
            _generator = generator;
            products = _generator.Generate(100);

        }
        /*
        Для того щоб вказати метод запиту для маршруту(ендпоінта) необхідно використовувати аннотації(атрибути)
        Для кожного HTTP методу існує свій атрибут
        CRUD - Create Read Update Delete
        [HttpGet] - для отримання даних (GET) (Read)
        [HttpPost] - для створення нових даних (POST) (Create) (Підтвердження даних(аутентифікація, авторизація))
        [HttpPut] - для оновлення інформації (Повне оновлення, ви оновюєте всі поля в об'єкті)
        [HttpPatch] - для часткового оновлення інформації (Наприклад: оновити тільки ім'я, чи номер телефону, чи назва продукту)
        [HttpDelete] - для видалення даних

        Додатково ви можете вказати маршрут самостійно, для цього використовуєте атрибут [Route]
        [Route("endpointname//*//*")]
        */

        /// <summary>
        /// Стартовий ендпоінт, який повертає привітання
        /// </summary>
        /// <remarks>
        /// Цей метод повертає JSON-об'єкт, з успіхом та вітанням
        /// Приклад запиту: GET /api/home/
        /// </remarks>
        /// <returns>OkObjectResult</returns>
        /// <response code="200">Успіх</response>
        
        
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new
            {
                Status=200,
                Message="Welcome to webapi"
            });
        }

         [HttpGet]
         [Route("products")]
        public IActionResult Products()
        {
            return Ok(new
            {
                Status=200,
                Message="Ok",
                Products = products
            });
        }
        
        [HttpGet]
        [Route("productQuery")]
        public IActionResult GetProductsIn([FromQuery] int?startFrom, [FromQuery] int? endsWith)
        {
            if (startFrom.HasValue)
            {
                if(startFrom.Value< 0 || startFrom.Value > products.Count - 1)
                {
                    return BadRequest(new
                    {
                        Status=400,
                        Message = $"Start from must be in 0-{products.Count}"
                    });
                }
                return Ok(products.Slice(startFrom.Value, (products.Count-1)-startFrom.Value));
            }
            if (endsWith.HasValue)
            {
                if(endsWith< products.Count || endsWith > products.Count - 1)
                {
                    return BadRequest(new
                    {
                        Status=400,
                        Message = $"endsWith must be in 0-{products.Count-1}"
                    });
                }
            }
            return NotFound();
        }

        //Реалізація рут параметру
        [HttpGet("products/{id}")]
        public IActionResult GetProductById(int id)
        {
            if(id > (products.Count-1)) return NotFound(new
            {
                Status = 404,
                Message = $"Product with id-{id} not found",
                Data = (object)null!
            });
            // return Ok(products[id]);
            return Ok(new
            {
                Status=200,
                Message="Ok",
                Data = products[id]
            });
        }


        [HttpGet]
        [Route("health-check")]
        [SwaggerOperation(
            Summary = "Перевірка стану сервера",
            Description="Перевіряє стан северу, якщо все гаразд повертає об'єкт(Статус: 200 та повідомлення), інакше ендпоінт не працює"
            // Tags = new []{"Home Controller", "Healt Check"}
        )]
        public IActionResult HealthCheck()
        {
            return Ok(new
            {
                Status=200,
                Message="Server is fine"
            });
        }
        /*
        IGenerator
         generate(int count)
         ProductGenerator: IGenerator
         prodcutGenerator.generate(1000) -> List<Product>
        */
    }
}