using archival_library_backend.Entities;

namespace archival_library_backend.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(AppUser user);

}
