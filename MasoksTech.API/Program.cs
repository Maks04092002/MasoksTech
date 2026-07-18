using MasoksTech.Infrastructure.Data;
using MasoksTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// Configuración de PostgreSQL
// ========================================
if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")));
}

// ========================================
// Configuración de JWT
// ========================================
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? "ClaveSuperSecretaParaMasoksTech1234567890!");

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
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Servicios
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Aquí irán las Inyecciones de dependencias (Ej: repositorios, servicios de aplicación)
// builder.Services.AddScoped<IProductoService, ProductoService>();

var app = builder.Build();

// ========================================
// Seed de datos iniciales
// ========================================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Seed de Rol si no existe
    if (!context.Roles.Any())
    {
        var rolAdmin = new Rol { Nombre = "Administrador" };
        var rolCliente = new Rol { Nombre = "Cliente" };
        context.Roles.AddRange(rolAdmin, rolCliente);
        context.SaveChanges();
    }

    if (!context.Categorias.Any())
    {
        context.Categorias.Add(new Categoria { Nombre = "Cargadores" });
        context.Categorias.Add(new Categoria { Nombre = "Protección" });

        context.Marcas.Add(new Marca { Nombre = "Apple" });
        context.Marcas.Add(new Marca { Nombre = "Samsung" });

        context.Productos.Add(new Producto
        {
            Nombre = "Funda Antigolpes Samsung S23",
            MarcaId = 2,
            CategoriaId = 2,
            Precio = 35.00m
        });

        context.SaveChanges();
    }
}

// ========================================
// Middleware
// ========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public partial class Program { }