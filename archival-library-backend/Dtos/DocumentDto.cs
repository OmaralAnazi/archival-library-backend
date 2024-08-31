namespace archival_library_backend.Dtos;

public class DocumentDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string authorName { get; set; }
    public DateTime PublicationDate { get; set; }
}
