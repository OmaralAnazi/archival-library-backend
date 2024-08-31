using archival_library_backend.Entities;

namespace archival_library_backend.Interfaces;

public interface IDocumentRepository
{
    Task<DocumentMetadata> AddDocumentAsync(DocumentMetadata document);
    Task<List<DocumentMetadata>> GetAllDocumentsAsync();
    Task<DocumentMetadata?> GetDocumentByIdAsync(int id);
    Task<List<DocumentMetadata>> GetAllUserDocumentsAsync(string userId);
    Task<bool> IsExistsAsync(int id);
    Task<bool> IsOwnedByAsync(int id, string userId);
    Task DeleteDocumentAsync(int id);

}
