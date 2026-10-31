using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace p48_lessons_asp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {

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
    }
}