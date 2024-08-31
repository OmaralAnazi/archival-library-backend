using archival_library_backend.Dtos;

namespace archival_library_backend.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllCategoriesDtosAsync();
}
