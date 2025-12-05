using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using InvoicingAPI.Application.DTO;
using BCrypt.Net;

using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;

namespace InvoicingAPI.Application.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<User?> RegisterAsync(RegisterUserDto dto)
    {
        var exists = await _context.Users.AnyAsync(x => x.Email == dto.Email);
        if (exists)
            return null;

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (user == null) return null;

        if (!VerifyPassword(dto.Password, user.PasswordHash))
            return null;

        return user;
    }

    public string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("name", user.FullName),
            //new Claim("companyId", user.CompanyId.ToString()),
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
    public async Task<bool> ResetPasswordAsync(string token, string newPassword)
    {
        var reset = await _context.PasswordResetTokens
            .FirstOrDefaultAsync(x => x.Token == token);

        if (reset == null || reset.IsUsed || reset.Expiry < DateTime.UtcNow)
            return false;

        // Fetch the user using GUID UserId
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == reset.UserId);

        if (user == null)
            return false;

        // Hash new password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

        reset.IsUsed = true;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string> ForgotPasswordAsync(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            // Don't expose user existence
            return "If this email exists, a reset link has been sent.";
        }

        var token = Guid.NewGuid().ToString();

        var resetToken = new PasswordResetToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,                 // GUID → GUID (correct)
            Token = token,
            Expiry = DateTime.UtcNow.AddMinutes(15),
            IsUsed = false
        };

        _context.PasswordResetTokens.Add(resetToken);
        await _context.SaveChangesAsync();

        return $"https://localhost:4200/reset-password?token={token}";
    }



}
