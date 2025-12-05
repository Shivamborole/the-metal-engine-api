using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaterialsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Materials?companyId=...
        [HttpGet]
        public async Task<IActionResult> GetMaterials([FromQuery] Guid companyId)
        {
            if (companyId == Guid.Empty)
                return BadRequest("companyId is required");

            var list = await _context.Materials
                .Where(x => x.CompanyId == companyId)
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Ok(list);
        }

        // POST: api/Materials
        [HttpPost]
        public async Task<IActionResult> AddMaterial([FromBody] Material dto)
        {
            dto.Id = Guid.NewGuid();
            dto.CreatedAt = DateTime.UtcNow;

            _context.Materials.Add(dto);
            await _context.SaveChangesAsync();

            return Ok(dto);
        }
    }
}
