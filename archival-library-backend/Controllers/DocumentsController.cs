using archival_library_backend.Dtos;
using archival_library_backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace archival_library_backend.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDocuments()
    {
        var documents = await _documentService.GetAllDocumentDtosAsync();
        return Ok(documents); 
    }

    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetADocumentsForUser()
    {
        var userId = User.FindFirstValue("userId");
        var documents = await _documentService.GetAllUserDocumentDtosAsync(userId);
        return Ok(documents);
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<IActionResult> UploadDocument([FromForm] UploadDocumentDto uploadDocumentDto, [FromForm] IFormFile file)
    {
        var userId = User.FindFirstValue("userId");
        var document = await _documentService.UploadDocumentAsync(uploadDocumentDto, file, userId);
        // TODO: replace with createdAt + ensure the doucment reutned as DTO
        return Ok(new { Message = "Document uploaded successfully!", Document = document });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteDocument([FromRoute] int id)
    {
        var userId = User.FindFirstValue("userId");
        await _documentService.DeleteDocumentAsync(id, userId);
        return NoContent();
    }
}
