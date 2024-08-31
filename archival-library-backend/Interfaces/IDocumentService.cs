using archival_library_backend.Dtos;
using archival_library_backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace archival_library_backend.Interfaces;

public interface IDocumentService
{
    Task<DocumentMetadata> UploadDocumentAsync(UploadDocumentDto uploadDocumentDto, IFormFile file, string userId);
    Task<List<DocumentDto>> GetAllDocumentDtosAsync();
    Task<FileStreamResult> GetDocumentFileForViewAsync(int id);
    Task<List<DocumentDto>> GetAllUserDocumentDtosAsync(string userId);
    Task DeleteDocumentAsync(int id, string userId);
}
