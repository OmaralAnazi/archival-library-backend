using archival_library_backend.Dtos;
using archival_library_backend.Interfaces;

namespace archival_library_backend.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesDtosAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();

        var categoriesDtos = categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();

        return categoriesDtos;
    }
}
