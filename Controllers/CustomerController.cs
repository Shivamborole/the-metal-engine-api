using InvoicingAPI.Application.DTO;
using InvoicingAPI.Application.Helpers;
using InvoicingAPI.Domain.Entities;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/customers?companyId=...
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] Guid companyId)
        {
            if (companyId == Guid.Empty)
                return BadRequest("CompanyId required.");

            var customers = await _context.Customers
                .Where(c => c.CompanyId == companyId)
                .OrderBy(c => c.CustomerName)
                .ToListAsync();

            return Ok(customers);
        }

        // GET api/customers/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.GSTNumber) &&
                !GstValidator.IsValid(dto.GSTNumber))
            {
                return BadRequest(new { message = "Invalid GST Number." });
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CompanyId = dto.CompanyId,

                CustomerName = dto.CustomerName,
                CompanyName = dto.CompanyName,
                CustomerType = dto.CustomerType,

                Email = dto.Email,
                Phone = dto.Phone,
                AlternatePhone = dto.AlternatePhone,

                GSTNumber = dto.GSTNumber?.Trim().ToUpper(),
                PANNumber = dto.PANNumber,

                BillingAddressLine1 = dto.BillingAddressLine1,
                BillingAddressLine2 = dto.BillingAddressLine2,
                BillingCity = dto.BillingCity,
                BillingState = dto.BillingState,
                BillingPincode = dto.BillingPincode,
                BillingCountry = dto.BillingCountry,

                ShippingSame = dto.ShippingSame,
                ShippingAddressLine1 = dto.ShippingSame ? dto.BillingAddressLine1 : dto.ShippingAddressLine1,
                ShippingAddressLine2 = dto.ShippingSame ? dto.BillingAddressLine2 : dto.ShippingAddressLine2,
                ShippingCity = dto.ShippingSame ? dto.BillingCity : dto.ShippingCity,
                ShippingState = dto.ShippingSame ? dto.BillingState : dto.ShippingState,
                ShippingPincode = dto.ShippingSame ? dto.BillingPincode : dto.ShippingPincode,
                ShippingCountry = dto.ShippingSame ? dto.BillingCountry : dto.ShippingCountry,

                CreditLimit = dto.CreditLimit,
                OpeningBalance = dto.OpeningBalance,
                Notes = dto.Notes,

                CreatedAt = DateTime.UtcNow
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Customer created successfully!",
                customerId = customer.Id
            });
        }

        // PUT api/customers/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(dto.GSTNumber) &&
                !GstValidator.IsValid(dto.GSTNumber))
            {
                return BadRequest(new { message = "Invalid GST Number." });
            }

            customer.CustomerName = dto.CustomerName;
            customer.CompanyName = dto.CompanyName;
            customer.CustomerType = dto.CustomerType;

            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.AlternatePhone = dto.AlternatePhone;

            customer.GSTNumber = dto.GSTNumber?.Trim().ToUpper();
            customer.PANNumber = dto.PANNumber;

            customer.BillingAddressLine1 = dto.BillingAddressLine1;
            customer.BillingAddressLine2 = dto.BillingAddressLine2;
            customer.BillingCity = dto.BillingCity;
            customer.BillingState = dto.BillingState;
            customer.BillingPincode = dto.BillingPincode;
            customer.BillingCountry = dto.BillingCountry;

            customer.ShippingSame = dto.ShippingSame;
            customer.ShippingAddressLine1 = dto.ShippingSame ? dto.BillingAddressLine1 : dto.ShippingAddressLine1;
            customer.ShippingAddressLine2 = dto.ShippingSame ? dto.BillingAddressLine2 : dto.ShippingAddressLine2;
            customer.ShippingCity = dto.ShippingSame ? dto.BillingCity : dto.ShippingCity;
            customer.ShippingState = dto.ShippingSame ? dto.BillingState : dto.ShippingState;
            customer.ShippingPincode = dto.ShippingSame ? dto.BillingPincode : dto.ShippingPincode;
            customer.ShippingCountry = dto.ShippingSame ? dto.BillingCountry : dto.ShippingCountry;

            customer.CreditLimit = dto.CreditLimit;
            customer.OpeningBalance = dto.OpeningBalance;
            customer.Notes = dto.Notes;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Customer updated successfully!" });
        }

        // DELETE api/customers/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Customer deleted." });
        }
    }
}
