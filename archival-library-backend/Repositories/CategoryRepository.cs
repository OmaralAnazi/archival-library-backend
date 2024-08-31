using archival_library_backend.Data;
using archival_library_backend.Entities;
using archival_library_backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace archival_library_backend.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Category.ToListAsync();
    }
}
