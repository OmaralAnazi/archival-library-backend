using archival_library_backend.Dtos;
using archival_library_backend.Entities;
using archival_library_backend.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace archival_library_backend.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = new AppUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PhoneNumber = registerDto.PhoneNumber,
            Birthdate = registerDto.Birthdate 
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var token = await _tokenService.CreateTokenAsync(user);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized("Invalid email or password.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized("Invalid email or password.");

        var token = await _tokenService.CreateTokenAsync(user);
        return Ok(new { token });
    }
}

