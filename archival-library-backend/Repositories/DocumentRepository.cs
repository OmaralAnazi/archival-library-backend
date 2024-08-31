using archival_library_backend.Data;
using archival_library_backend.Entities;
using archival_library_backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace archival_library_backend.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _context;

    public DocumentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DocumentMetadata>> GetAllDocumentsAsync()
    {
        return await _context.DocumentMetadatas
            .Include(d => d.Category)
            .Include(d => d.AppUser)
            .ToListAsync();
    }

    public async Task<List<DocumentMetadata>> GetAllUserDocumentsAsync(string userId)
    {
        return await _context.DocumentMetadatas
            .Include(d => d.Category)
            .Include(d => d.AppUser)
            .Where(d => d.AppUser.Id == userId)
            .ToListAsync();
    }

    public async Task<DocumentMetadata> AddDocumentAsync(DocumentMetadata document)
    {
        _context.DocumentMetadatas.Add(document);
        await _context.SaveChangesAsync();
        return document;
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.DocumentMetadatas.AnyAsync(d => d.Id == id);
    }

    public async Task<bool> IsOwnedByAsync(int id, string userId)
    {
        return await _context.DocumentMetadatas.AnyAsync(d => d.Id == id && d.AppUserId == userId);
    }

    public async Task DeleteDocumentAsync(int id)
    {
        var document = await _context.DocumentMetadatas.FindAsync(id);
        if (document != null)
        {
            _context.DocumentMetadatas.Remove(document);
            await _context.SaveChangesAsync();
        }
    }
}
