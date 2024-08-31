using Microsoft.AspNetCore.Identity;

namespace archival_library_backend.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Birthdate { get; set; }  

}
