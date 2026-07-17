using MasoksTech.API.Data;
using MasoksTech.API.Interfaces;
using MasoksTech.API.Models;
using MasoksTech.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar el AppDbContext utilizando la base de datos In-Memory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MasoksTechInMemoryDb"));

builder.Services.AddControllers();

// Configuración básica de Endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccesoriosService, AccesoriosService>();


var app = builder.Build();

// 2. Pre-cargar (Seed) datos iniciales
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Solo agregamos si la BD está vacía
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

// Configurar Swagger para desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
// ... Todo tu código existente en Program.cs ...

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public partial class Program { }