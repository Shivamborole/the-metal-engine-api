using System.Text;
using InvoicingAPI.Application.Helpers;
using InvoicingAPI.Application.Services;
using InvoicingAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------
// 1. Add CORS (Required to allow Angular frontend)
// ---------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// ---------------------------------------------------
// 2. Add Controllers, Swagger
// ---------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------------------------------------
// 3. Database
// ---------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


// ---------------------------------------------------
// 4. JWT Authentication
// ---------------------------------------------------
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
QuestPDF.Settings.License = LicenseType.Community;
// ---------------------------------------------------
// 5. Register Services
// ---------------------------------------------------
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IInvoiceNumberGenerator, InvoiceNumberGenerator>();
builder.Services.AddScoped<IDeliveryChallanNumberGenerator, DeliveryChallanNumberGenerator>();

var app = builder.Build();

// ---------------------------------------------------
// 6. Enable CORS BEFORE Authentication
// ---------------------------------------------------
app.UseCors("AllowAngular");

// ---------------------------------------------------
// 7. Swagger
// ---------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ---------------------------------------------------
// 8. HTTP pipeline
// ---------------------------------------------------
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
