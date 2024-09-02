using archival_library_backend.Dtos;
using archival_library_backend.Entities;
using archival_library_backend.Exceptions;
using archival_library_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace archival_library_backend.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<List<DocumentDto>> GetAllDocumentDtosAsync()
    {
        var documents = await _documentRepository.GetAllDocumentsAsync();

        var documentDtos = documents.Select(doc => new DocumentDto
        {
            Id = doc.Id,
            Title = doc.Title,
            Description = doc.Description,
            CategoryName = doc.Category?.Name ?? "Uncategorized", 
            authorName = $"{doc.AppUser?.FirstName} {doc.AppUser?.LastName}", 
            PublicationDate = doc.PublicationDate
        }).ToList();

        return documentDtos;
    }

    public async Task<List<DocumentDto>> GetAllUserDocumentDtosAsync(string userId)
    {
        var documents = await _documentRepository.GetAllUserDocumentsAsync(userId);

        var documentDtos = documents.Select(doc => new DocumentDto
        {
            Id = doc.Id,
            Title = doc.Title,
            Description = doc.Description,
            CategoryName = doc.Category?.Name ?? "Uncategorized",
            authorName = $"{doc.AppUser?.FirstName} {doc.AppUser?.LastName}",
            PublicationDate = doc.PublicationDate
        }).ToList();

        return documentDtos;
    }

    public async Task<FileStreamResult> GetDocumentFileForViewAsync(int id)
    {
        var document = await _documentRepository.GetDocumentByIdAsync(id);
        if (document == null)
            throw new NotFoundException("Document not found.");

        var filePath = document.FilePath;

        if (!System.IO.File.Exists(filePath))
            throw new NotFoundException("File not found on the server.");

        // Determine the MIME type of the file
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "application/octet-stream"; // default if type is unknown
        }

        // Open file stream to be returned
        var fileStream = System.IO.File.OpenRead(filePath);

        // Set the content disposition to 'inline' to display the file in the browser
        var result = new FileStreamResult(fileStream, contentType)
        {
            FileDownloadName = Path.GetFileName(filePath),
            // Ensure the file is displayed in the browser rather than downloaded
            EnableRangeProcessing = true // Allows seeking, useful for media files
        };

        result.FileDownloadName = null; // Optional: don't suggest a filename when viewing

        return result;
    }

    public async Task<DocumentMetadata> UploadDocumentAsync(UploadDocumentDto uploadDocumentDto, IFormFile file, string userId)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var fullFilePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(fullFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var document = new DocumentMetadata
        {
            Title = uploadDocumentDto.Title,
            Description = uploadDocumentDto.Description,
            CategoryId = uploadDocumentDto.CategoryId,
            FilePath = fullFilePath,
            AppUserId = userId,
        };

        return await _documentRepository.AddDocumentAsync(document);
    }

    public async Task DeleteDocumentAsync(int id, string userId)
    {
        var document  = await _documentRepository.GetDocumentByIdAsync(id);
        if (document == null) throw new NotFoundException("Document is not found");

        var ownedBy = await _documentRepository.IsOwnedByAsync(id, userId);
        if (!ownedBy) throw new ForbiddenException("This is not your document");

        if (System.IO.File.Exists(document.FilePath))
        {
            try
            {
                System.IO.File.Delete(document.FilePath);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the file.");
            }
        }

        await _documentRepository.DeleteDocumentAsync(id);
    }

}
