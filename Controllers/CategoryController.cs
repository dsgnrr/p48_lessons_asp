using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Data;
using p48_lessons_asp.Data.Entity;
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
            var validations = await _createCategoryValidator.GetErrorsAsync(model);

            if (validations != null)
            {
                return new ObjectResult(validations) { StatusCode = 400 };
            }
            
            Category category = new()
            {
                Title = model.Title,
                Slug = _slugService.GetSlug(model.Title),
                Description = model.Description
            };
            if(Guid.TryParse(model.ParentCategoryId, out var parsed)) category.ParentCategoryId = parsed;
            try
            {
                await _dataContext.Categories.AddAsync(category);
                await _dataContext.SaveChangesAsync();
                return new ObjectResult(new
                {
                    StatusCdoe = 201,
                    Message = "Created",
                    Data = category
                });
            }
            catch (OperationCanceledException ex)
            {
                System.Console.WriteLine($"Created category error: {ex.Message}");
                return new ObjectResult(new
                {
                    StatusCode = 500,
                    Message = "See server logs",
                    Data = (object)null!
                })
                { StatusCode = 500 };
            }
        }
    }
}
