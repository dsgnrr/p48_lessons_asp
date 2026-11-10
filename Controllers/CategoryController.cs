using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Data;
using p48_lessons_asp.Models.CategoryRequestModels;
using p48_lessons_asp.Services.FluentValidation;
using p48_lessons_asp.Services.Slug;

namespace p48_lessons_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ISlugService _slugService;
        private readonly DataContext _dataContext;
        IValidator<CreateCategoryModel> _createCategoryValidator;
        public CategoryController(
            ISlugService slugService,
            DataContext dataContext,
            IValidator<CreateCategoryModel> createCategoryValidator
            )
        {
            _slugService = slugService;
            _dataContext = dataContext;
            _createCategoryValidator = createCategoryValidator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryModel model)
        {
            var validations = _createCategoryValidator.GetErrors(model);

            if (validations != null)
            {
                return new ObjectResult(validations) { StatusCode = 400 };
            }
            return Ok();
        }
    }
}
