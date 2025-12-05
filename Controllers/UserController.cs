using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("companies")]
        public async Task<IActionResult> GetUserCompanies()
        {
            //var userId = User.FindFirst("sub")?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
 ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userId == null)
                return Unauthorized();

            var companies = await _context.UserCompanyMaps
        .Where(x => x.UserId == Guid.Parse(userId))
        .Include(x => x.Company)
        .Select(x => new
        {
            companyId = x.CompanyId,
            companyName = x.Company.Name,
            isSelected = x.IsActive
        })
        .ToListAsync();

            return Ok(companies);
        }
    }

}
