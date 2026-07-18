using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.Infrastructure.Data;
using MasoksTech.Domain.Entities;
using MasoksTech.Application.DTOs;
using MasoksTech.API.Controllers;
using Xunit;

namespace MasoksTech.Specs;

public class PedidosControllerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);

        // Seed
        var producto = new Producto 
        { 
            Id = 1, 
            Nombre = "Funda", 
            Precio = 10, 
            Inventario = new Inventario { Stock = 5 } 
        };
        context.Productos.Add(producto);
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task RegistrarPedido_ReduceInventario_Correctamente()
    {
        // Arrange
        using var context = GetDbContext();
        var controller = new PedidosController(context);

        var dto = new CrearPedidoDto(
            "Juan Perez",
            "juan@test.com",
            "123456789",
            "Calle 123",
            new List<DetallePedidoDto> { new DetallePedidoDto(1, 2) }
        );

        // Act
        var result = await controller.RegistrarPedido(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<RespuestaPedidoDto>(okResult.Value);
        Assert.Equal(20m, response.Total);

        var productoDb = await context.Productos.Include(p => p.Inventario).FirstAsync(p => p.Id == 1);
        Assert.Equal(3, productoDb.Inventario!.Stock); // 5 - 2
    }

    [Fact]
    public async Task RegistrarPedido_FallaSiNoHayStock()
    {
        // Arrange
        using var context = GetDbContext();
        var controller = new PedidosController(context);

        var dto = new CrearPedidoDto(
            "Juan Perez",
            "juan@test.com",
            "123456789",
            "Calle 123",
            new List<DetallePedidoDto> { new DetallePedidoDto(1, 10) } // Pide 10, hay 5
        );

        // Act
        var result = await controller.RegistrarPedido(dto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}
