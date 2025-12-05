using InvoicingAPI.Application.DTO;

using InvoicingAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var user = await _auth.RegisterAsync(dto);

        if (user == null)
        {
            return BadRequest(new
            {
                message = "Email already exists"
            });
        }

        return Ok(new
        {
            message = "User registered successfully"
        });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        
        var user = await _auth.LoginAsync(dto);
        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = _auth.GenerateJwtToken(user);

        return Ok(new { token });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        var message = await _auth.ForgotPasswordAsync(dto.Email);
        return Ok(new { message });
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var success = await _auth.ResetPasswordAsync(dto.Token, dto.NewPassword);

        if (!success)
            return BadRequest("Invalid or expired token");

        return Ok(new { message = "Password has been reset successfully" });
    }


}
