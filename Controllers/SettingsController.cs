using InvoicingAPI.Application.DTO;
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
    public class SettingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/settings/invoice-number?companyId=...
        [HttpGet("invoice-number")]
        public async Task<IActionResult> GetInvoiceNumberSettings([FromQuery] Guid companyId)
        {
            if (companyId == Guid.Empty)
                return BadRequest("CompanyId is required.");

            var settings = await _context.InvoiceNumberSettings
                .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.IsDefault);

            // If not present, seed a default row
            if (settings == null)
            {
                settings = new InvoiceNumberSettings
                {
                    Id = Guid.NewGuid(),
                    CompanyId = companyId,
                    Prefix = "INV-",
                    Suffix = "",
                    Padding = 5,
                    ResetFrequency = NumberResetFrequency.Yearly,
                    CurrentNumber = 0,
                    CurrentYear = DateTime.UtcNow.Year,
                    CurrentMonth = DateTime.UtcNow.Month,
                    IsDefault = true
                };

                _context.InvoiceNumberSettings.Add(settings);
                await _context.SaveChangesAsync();
            }

            var dto = new InvoiceNumberSettingsDto
            {
                Id = settings.Id,
                CompanyId = settings.CompanyId,
                Prefix = settings.Prefix,
                Suffix = settings.Suffix,
                Padding = settings.Padding,
                ResetFrequency = settings.ResetFrequency,
                CurrentNumber = settings.CurrentNumber,
                CurrentYear = settings.CurrentYear,
                CurrentMonth = settings.CurrentMonth
            };

            return Ok(dto);
        }

        // PUT api/settings/invoice-number/{id}
        [HttpPut("invoice-number/{id:guid}")]
        public async Task<IActionResult> UpdateInvoiceNumberSettings(
            Guid id,
            [FromBody] UpdateInvoiceNumberSettingsDto dto)
        {
            var settings = await _context.InvoiceNumberSettings
                .FirstOrDefaultAsync(x => x.Id == id);

            if (settings == null)
                return NotFound("Settings not found.");

            settings.Prefix = dto.Prefix?.Trim() ?? "";
            settings.Suffix = dto.Suffix?.Trim() ?? "";
            settings.Padding = dto.Padding <= 0 ? 5 : dto.Padding;
            settings.ResetFrequency = dto.ResetFrequency;

            await _context.SaveChangesAsync();
            return Ok();
        }
      

    }
}
