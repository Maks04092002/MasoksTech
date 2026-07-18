using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.Infrastructure.Data;
using MasoksTech.Domain.Entities;
using MasoksTech.Application.DTOs;
using MasoksTech.API.Controllers;
using Xunit;

namespace MasoksTech.Specs;

public class InventarioControllerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);

        context.Productos.Add(new Producto { Id = 1, Nombre = "Prod 1", Precio = 10, CategoriaId = 1, MarcaId = 1 });
        context.Inventarios.Add(new Inventario { Id = 1, ProductoId = 1, Stock = 10 });
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task RegistrarVenta_ReturnsOk_WithDecreasedStock()
    {
        using var context = GetDbContext();
        var controller = new InventarioController(context);
        var dto = new RegistrarVentaDto(1, 2);

        var result = await controller.RegistrarVenta(dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var resDto = Assert.IsType<RespuestaVentaDto>(okResult.Value);
        
        Assert.Equal(8, resDto.StockResultante);
    }

    [Fact]
    public async Task RegistrarVenta_ReturnsBadRequest_WhenNoStock()
    {
        using var context = GetDbContext();
        var controller = new InventarioController(context);
        var dto = new RegistrarVentaDto(1, 25);

        var result = await controller.RegistrarVenta(dto);

        var badResult = Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}
