using archival_library_backend.Dtos;
using archival_library_backend.Entities;

namespace archival_library_backend.Interfaces;

public interface IDocumentService
{
    Task<DocumentMetadata> UploadDocumentAsync(UploadDocumentDto uploadDocumentDto, IFormFile file, string userId);
    Task<List<DocumentDto>> GetAllDocumentDtosAsync();
    Task<List<DocumentDto>> GetAllUserDocumentDtosAsync(string userId);
    Task DeleteDocumentAsync(int id, string userId);
}
