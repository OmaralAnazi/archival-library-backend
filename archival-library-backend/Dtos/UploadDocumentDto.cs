using System.ComponentModel.DataAnnotations;

namespace archival_library_backend.Dtos;

public class UploadDocumentDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
