using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InvoicingAPI.Application.DTO;
using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompanyController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto dto)
        {
            // 1. Validate request DTO
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Company name is required" });

            // 2. Extract userId from JWT
            var userIdString =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized(new { message = "User ID missing from token." });

            var userId = Guid.Parse(userIdString);

            // 3. Check duplicate company name for same user
            bool companyExists = await _context.UserCompanyMaps
                .AnyAsync(x => x.UserId == userId && x.Company.Name == dto.Name);

            if (companyExists)
                return BadRequest(new { message = "A company with the same name already exists." });

            // 4. Create company entity
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                GSTNumber = dto.GSTNumber,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                CreatedAt = DateTime.UtcNow,
                OwnerUserId = userId
            };

            await _context.Companies.AddAsync(company);

            // 5. Fetch Owner role from RoleMaster
            var ownerRole = await _context.RoleMasters
                .FirstOrDefaultAsync(r => r.RoleName == "Owner");

            if (ownerRole == null)
                return StatusCode(500, new { message = "Owner role not found. Please seed roles." });

            // 6. Map user → company → role
            var map = new UserCompanyMap
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CompanyId = company.Id,
                RoleId = ownerRole.Id,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _context.UserCompanyMaps.AddAsync(map);

            // 7. Save everything cleanly
            await _context.SaveChangesAsync();

            // 8. Final response
            return Ok(new
            {
                message = "Company created successfully",
                companyId = company.Id
            });
        }

        //[Authorize]
        //[HttpPost("Create")]
        //public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto dto)
        //{
        //    // 1. Extract userId from JWT
        //    //var userId = User.FindFirst("id")?.Value;
        //    //var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        //     ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized("User ID missing from token.");

        //    var uid = Guid.Parse(userId);

        //    // 2. Create company entity
        //    var company = new Company
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = dto.Name,
        //        GSTNumber = dto.GSTNumber,
        //        Address = dto.Address,
        //        City = dto.City,
        //        State = dto.State,
        //        Country = dto.Country,
        //        CreatedAt = DateTime.UtcNow,
        //        OwnerUserId = uid
        //    };

        //    // 3. Add company
        //    await _context.Companies.AddAsync(company);

        //    // 4. Add mapping (User → Company)
        //    //await _context.UserCompanyMaps.AddAsync(new UserCompanyMap
        //    //{
        //    //    UserId = uid,
        //    //    CompanyId = company.Id
        //    //});

        //    await _context.UserCompanyMaps.AddAsync(new UserCompanyMap
        //    {
        //        UserId = uid,
        //        CompanyId = company.Id,
        //        Role = "Admin",   // TEMPORARY FIX
        //        CreatedAt = DateTime.UtcNow
        //    });

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch
        //    (Exception ex)
        //    { }

        //    // 5. Save everything
        //    await _context.SaveChangesAsync();

        //    // 6. Return response
        //    return Ok(new
        //    {
        //        message = "Company created successfully.",
        //        companyId = company.Id
        //    });
        //}

        //[Authorize]
        //[HttpGet("list")]
        //public async Task<IActionResult> GetMyCompanies()
        //{
        //    //var userId = User.FindFirst("sub")?.Value;
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        //     ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized("User ID missing in token.");

        //    var uid = Guid.Parse(userId);

        //    var companies = await _context.UserCompanyMaps
        //        .Where(x => x.UserId == uid)
        //        .Select(x => x.Company)
        //        .ToListAsync();

        //    if (companies.Count == 0)
        //    {
        //        return NotFound(new
        //        {
        //            message = "No companies mapped with you. Please add company."
        //        });
        //    }

        //    return Ok(companies);
        //}
        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> GetUserCompanies()
        {
            var userIdString =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var userId = Guid.Parse(userIdString);

            var companies = await _context.UserCompanyMaps
                .Where(m => m.UserId == userId)
                .Select(m => new
                {
                    m.Company.Id,
                    m.Company.Name,
                    m.Company.GSTNumber,
                    m.Company.Address,
                    m.Company.City,
                    m.Company.State,
                    m.Company.Country,

                    isActive = m.IsActive   // ✔ This now comes from UserCompanyMap
                })
                .ToListAsync();

            return Ok(companies);
        }


        [Authorize]
        [HttpPost("set-active")]
        public async Task<IActionResult> SetActiveCompany([FromBody] setActiveCompanyDto dto)
        {
            var userIdString =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized(new { message = "User ID missing." });

            var userId = Guid.Parse(userIdString);

            // 1. Find all mappings for this user
            var mappings = await _context.UserCompanyMaps
                .Where(x => x.UserId == userId)
                .ToListAsync();

            // 2. Deactivate all
            foreach (var m in mappings)
                m.IsActive = false;

            // 3. Activate selected one
            var target = mappings.FirstOrDefault(m => m.CompanyId == dto.CompanyId);
            if (target == null)
                return BadRequest(new { message = "Invalid company selected." });

            target.IsActive = true;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Company activated successfully." });
        }

        [Authorize]
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveCompany()
        {
            var userIdString =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var userId = Guid.Parse(userIdString);

            var active = await _context.UserCompanyMaps
                .Where(m => m.UserId == userId && m.IsActive)
                .Select(m => new
                {
                    companyId = m.CompanyId,
                    companyName = m.Company.Name
                })
                .FirstOrDefaultAsync();

            if (active == null)
                return Ok(null);

            return Ok(active);
        }



    }

}
