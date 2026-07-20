using MasoksTech.API.Data;
using MasoksTech.API.Interfaces;
using MasoksTech.API.Models;
using MasoksTech.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// Configuración de PostgreSQL
// ========================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Servicios
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias
builder.Services.AddScoped<IAccesoriosService, AccesoriosService>();

var app = builder.Build();

// ========================================
// Seed de datos iniciales
// ========================================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // ESTA LÍNEA CREA LAS TABLAS AUTOMÁTICAMENTE EN RAILWAY
    context.Database.EnsureCreated();

    if (!context.Categorias.Any())
    {
        context.Categorias.Add(new Categoria { Id = 1, Nombre = "Cargadores" });
        context.Categorias.Add(new Categoria { Id = 2, Nombre = "Protección" });

        context.Marcas.Add(new Marca { Id = 1, Nombre = "Apple" });
        context.Marcas.Add(new Marca { Id = 2, Nombre = "Samsung" });

        context.Accesorios.Add(new Accesorio
        {
            Id = 1,
            Nombre = "Funda Antigolpes Samsung S23",
            MarcaId = 2,
            CategoriaId = 2,
            Precio = 35.00m,
            Stock = 15,
            StockMinimo = 5
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

app.UseAuthorization();

app.MapControllers();

app.Run();

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public partial class Program { }
