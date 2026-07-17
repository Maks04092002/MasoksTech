using Xunit;
using Microsoft.EntityFrameworkCore;
using MasoksTech.API.Controllers;
using MasoksTech.API.Data;
using MasoksTech.API.DTOs;
using MasoksTech.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MasoksTech.Specs;

public class InventarioTests
{
    private AppDbContext ObtenerContextoEnMemoria()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // BD limpia por cada prueba
            .Options;

        var context = new AppDbContext(options);

        // Cargar datos semilla
        context.Categorias.Add(new Categoria { Id = 1, Nombre = "Protección" });
        context.Marcas.Add(new Marca { Id = 1, Nombre = "Samsung" });
        context.Accesorios.Add(new Accesorio
        {
            Id = 99,
            Nombre = "Funda Antigolpes Samsung S23",
            MarcaId = 1,
            CategoriaId = 1,
            Precio = 50.00m,
            Stock = 15,
            StockMinimo = 5
        });
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task Escenario_Descuento_Automatico_De_Stock_Tras_Venta()
    {
        // Dado que (Arrange)
        var context = ObtenerContextoEnMemoria();
        var controller = new InventarioController(context);
        var dtoVenta = new RegistrarVentaDto(AccesorioId: 99, Cantidad: 2);

        // Cuando (Act)
        var result = await controller.RegistrarVenta(dtoVenta);

        // Entonces (Assert)
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var respuesta = Assert.IsType<RespuestaVentaDto>(okResult.Value);

        Assert.Equal(13, respuesta.StockResultante); // 15 - 2 = 13
        Assert.False(respuesta.AlertaStockMinimo);   // 13 sigue siendo mayor que el mínimo (5)
    }

    [Fact]
    public async Task Escenario_Impedir_Venta_Si_No_Hay_Stock()
    {
        // Dado que (Arrange)
        var context = ObtenerContextoEnMemoria();
        var controller = new InventarioController(context);
        var dtoVentaExcesiva = new RegistrarVentaDto(AccesorioId: 99, Cantidad: 20); // Pide 20 y solo hay 15

        // Cuando (Act)
        var result = await controller.RegistrarVenta(dtoVentaExcesiva);

        // Entonces (Assert)
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}