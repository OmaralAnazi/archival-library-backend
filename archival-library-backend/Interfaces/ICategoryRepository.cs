using archival_library_backend.Entities;

namespace archival_library_backend.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
}
