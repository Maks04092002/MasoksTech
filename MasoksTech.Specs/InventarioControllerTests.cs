using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.API.Controllers;
using MasoksTech.API.Data;
using MasoksTech.API.Models;
using MasoksTech.API.DTOs;

namespace MasoksTech.Specs;

public class InventarioControllerTests
{
    private AppDbContext CrearContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        // Insertamos un producto con stock inicial de 10 y stock mínimo de 5
        context.Accesorios.Add(new Accesorio
        {
            Id = 1,
            Nombre = "Audífonos Bluetooth",
            Precio = 50,
            Stock = 10,
            StockMinimo = 5, // Importante para probar tu alerta
            CategoriaId = 1,
            MarcaId = 1
        });
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task RegistrarVenta_AccesorioNoExiste_RetornaNotFound()
    {
        // Arrange
        var context = CrearContexto();
        var controller = new InventarioController(context);
        var dto = new RegistrarVentaDto(99, 1); // ID 99 no existe

        // Act
        var result = await controller.RegistrarVenta(dto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task RegistrarVenta_SinStockSuficiente_RetornaBadRequest()
    {
        // Arrange
        var context = CrearContexto();
        var controller = new InventarioController(context);

        // Intentamos vender 20, pero solo hay 10 en stock
        var dto = new RegistrarVentaDto(1, 20);

        // Act
        var result = await controller.RegistrarVenta(dto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task RegistrarVenta_ExitosaSinAlerta_RetornaOk()
    {
        // Arrange
        var context = CrearContexto();
        var controller = new InventarioController(context);

        // Vendemos 2 (Stock bajará a 8. Como el mínimo es 5, NO hay alerta)
        var dto = new RegistrarVentaDto(1, 2);

        // Act
        var result = await controller.RegistrarVenta(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var respuesta = Assert.IsType<RespuestaVentaDto>(okResult.Value);

        Assert.False(respuesta.AlertaStockMinimo); // Verificamos que la alerta sea falsa
        Assert.Equal(8, respuesta.StockResultante);
    }

    [Fact]
    public async Task RegistrarVenta_ExitosaConAlerta_RetornaOk()
    {
        // Arrange
        var context = CrearContexto();
        var controller = new InventarioController(context);

        // Vendemos 6 (Stock bajará a 4. Como el mínimo es 5, SI hay alerta)
        var dto = new RegistrarVentaDto(1, 6);

        // Act
        var result = await controller.RegistrarVenta(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var respuesta = Assert.IsType<RespuestaVentaDto>(okResult.Value);

        Assert.True(respuesta.AlertaStockMinimo); // Verificamos que la alerta sea verdadera
        Assert.Equal(4, respuesta.StockResultante);
    }
}