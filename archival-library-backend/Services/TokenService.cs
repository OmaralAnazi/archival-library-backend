using archival_library_backend.Entities;
using archival_library_backend.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace archival_library_backend.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> CreateTokenAsync(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim("userId", user.Id),
            new Claim("email", user.Email),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName),
            new Claim("phoneNumber", user.PhoneNumber),
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60), 
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
