using System.Text;
using InvoicingAPI.Application.Helpers;
using InvoicingAPI.Application.Services;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------
// 1. CORS (Frontend Access)
// -------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4200",
                "https://shivam-borole.netlify.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// -------------------------------------------
// 2. Controllers + Swagger
// -------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------------------------------
// 3. Database
// Azure will use the connection string from
// Configuration → Application Settings
// -------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// -------------------------------------------
// 4. JWT Authentication
// -------------------------------------------
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;   // Azure handles HTTPS termination
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

// PDF license
QuestPDF.Settings.License = LicenseType.Community;

// -------------------------------------------
// 5. Dependency Injection
// -------------------------------------------
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IInvoiceNumberGenerator, InvoiceNumberGenerator>();
builder.Services.AddScoped<IDeliveryChallanNumberGenerator, DeliveryChallanNumberGenerator>();

var app = builder.Build();

// -------------------------------------------
// 6. Global Exception Handling (Clean API Errors)
// -------------------------------------------
app.UseExceptionHandler("/error");

// -------------------------------------------
// 7. CORS (must be before auth)
// -------------------------------------------
app.UseCors("AllowAngular");

// -------------------------------------------
// 8. Swagger (Enabled in Production also)
// -------------------------------------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoicing API v1");
    c.RoutePrefix = "swagger";  // so Swagger loads at /swagger
});

// -------------------------------------------
// 9. HTTP Pipeline
// -------------------------------------------
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// -------------------------------------------
// 10. Run App
// -------------------------------------------
app.Run();
