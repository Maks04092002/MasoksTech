using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.Infrastructure.Data;
using MasoksTech.Domain.Entities;
using MasoksTech.API.Controllers;
using Xunit;

namespace MasoksTech.Specs;

public class ProductosControllerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);

        context.Categorias.Add(new Categoria { Id = 1, Nombre = "Cat 1" });
        context.Categorias.Add(new Categoria { Id = 2, Nombre = "Cat 2" });
        context.Marcas.Add(new Marca { Id = 1, Nombre = "Marca 1" });
        
        context.Productos.Add(new Producto { Id = 1, Nombre = "Producto 1", Precio = 10, CategoriaId = 1, MarcaId = 1 });
        context.Productos.Add(new Producto { Id = 2, Nombre = "Producto 2", Precio = 20, CategoriaId = 2, MarcaId = 1 });
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task GetProductos_ReturnsAllProductos()
    {
        using var context = GetDbContext();
        
        var controller = new ProductosController(context);

        var result = await controller.GetProductos();

        var productos = Assert.IsAssignableFrom<IEnumerable<Producto>>(result.Value);
        Assert.Equal(2, productos.Count());
    }

    [Fact]
    public async Task GetProducto_ReturnsNotFound_WhenIdDoesNotExist()
    {
        using var context = GetDbContext();
        var controller = new ProductosController(context);

        var result = await controller.GetProducto(99);

        Assert.IsType<NotFoundResult>(result.Result);
    }
}
