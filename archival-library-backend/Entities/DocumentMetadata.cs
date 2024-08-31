using System.ComponentModel.DataAnnotations;

namespace archival_library_backend.Entities;

public class DocumentMetadata
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PublicationDate { get; set; } = DateTime.Now;
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public string FilePath { get; set; } 
}
