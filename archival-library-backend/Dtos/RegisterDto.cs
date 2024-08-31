using System.ComponentModel.DataAnnotations;

namespace archival_library_backend.Dtos;

public class RegisterDto
{
    [Required]
    [MinLength(3)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    // Optimization: Ideally, a custom attribute should be used to validate the date, ensuring it's not in the future, but this will be skipped for now.
    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Please enter a valid date in the format yyyy-MM-dd.")]
    public string Birthdate { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}