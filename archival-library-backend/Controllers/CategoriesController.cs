using archival_library_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace archival_library_backend.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesDtosAsync();
        return Ok(categories);
    }
}
