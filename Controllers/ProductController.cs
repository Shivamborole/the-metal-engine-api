using InvoicingAPI.Application.DTO;
using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] Guid companyId)
        {
            var data = await _context.Products
                .Where(x => x.CompanyId == companyId)
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Model binding failed",
                    errors = ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new {
                            Field = e.Key,
                            Error = e.Value.Errors.First().ErrorMessage
                        })
                });
            }

            try
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    CompanyId = dto.CompanyId,
                    Name = dto.Name,
                    Sku = dto.Sku,
                    ProductType = dto.ProductType,
                    Category = dto.Category,
                    HsnOrSac = dto.HsnOrSac,

                    UnitPrice = dto.UnitPrice,
                    SellingPrice = dto.SellingPrice,
                    PurchasePrice = dto.PurchasePrice,
                    GstRate = dto.GstRate,

                    SellingUnit = dto.SellingUnit,
                    IsTaxInclusive = dto.IsTaxInclusive,
                    IsActive = dto.IsActive,

                    Description = dto.Description
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }

}
