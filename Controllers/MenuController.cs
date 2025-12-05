using InvoicingAPI.Application.DTO;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetMenu()
        //{
        //    var menu = await _context.MenuItems
        //        .Where(x => x.IsActive)
        //        .OrderBy(x => x.Order)
        //        .ToListAsync();

        //    return Ok(menu);
        //}

        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            var menu = await _context.MenuItems
                .Where(x => x.IsActive)
                .OrderBy(x => x.Order)
                .Select(x => new MenuItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Icon = x.Icon,
                    Route = x.Route,
                    Order = x.Order,
                    IsActive = x.IsActive,
                    ParentId = x.ParentId
                })
                .ToListAsync();

            return Ok(menu);
        }


    }
}
