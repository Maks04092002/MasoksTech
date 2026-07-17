using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.API.Controllers;
using MasoksTech.API.Data;
using MasoksTech.API.Models;
using MasoksTech.API.DTOs;
using MasoksTech.API.Services;

namespace MasoksTech.Specs;

public class AccesoriosControllerTests
{
    // Método auxiliar para preparar la base de datos de prueba
    private AppDbContext CrearContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        // Insertamos Marcas y Categorías para no fallar las validaciones del controlador
        context.Marcas.Add(new Marca { Id = 1, Nombre = "Apple" });
        context.Categorias.Add(new Categoria { Id = 1, Nombre = "Cables" });

        context.Accesorios.Add(new Accesorio
        {
            Id = 1,
            Nombre = "Funda de Prueba",
            Precio = 10,
            Stock = 5,
            CategoriaId = 1,
            MarcaId = 1
        });
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task ObtenerTodos_RetornaListaDeAccesorios()
    {
        // Arrange

        var context = CrearContexto();
        var service = new AccesoriosService(context);       // 1. Creamos el servicio
        var controller = new AccesoriosController(service); // 2. Le pasamos el servicio al controlador

        // Act
        var result = await controller.ObtenerTodos();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var lista = Assert.IsAssignableFrom<IEnumerable<AccesorioResponseDto>>(actionResult.Value);
        Assert.NotEmpty(lista);
    }

    [Fact]
    public async Task Crear_ConDatosValidos_RetornaCreated()
    {
        // Arrange

        var context = CrearContexto();
        var service = new AccesoriosService(context);       // 1. Creamos el servicio
        var controller = new AccesoriosController(service); // 2. Le pasamos el servicio al controlador

        // C# Records: (Nombre, MarcaId, CategoriaId, Precio, StockInicial)
        var nuevoDto = new CrearAccesorioDto("Cargador Test", 1, 1, 20m, 10);

        // Act
        var result = await controller.Crear(nuevoDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var valor = Assert.IsType<AccesorioResponseDto>(actionResult.Value);
        Assert.Equal("Cargador Test", valor.Nombre);
    }

    [Fact]
    public async Task Crear_SinNombre_RetornaBadRequest()
    {
        // Arrange
        var context = CrearContexto();
        var service = new AccesoriosService(context);       // 1. Creamos el servicio
        var controller = new AccesoriosController(service); // 2. Le pasamos el servicio al controlador

        // Enviamos nombre vacío en el primer parámetro
        var nuevoDto = new CrearAccesorioDto("", 1, 1, 20m, 10);

        // Act
        var result = await controller.Crear(nuevoDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("El nombre del accesorio es obligatorio.", badRequestResult.Value);
    }
    [Fact]
    public async Task Crear_ConMarcaOCategoriaInvalida_RetornaBadRequest()
    {
        // Arrange
        var context = CrearContexto();
        var service = new AccesoriosService(context);       // 1. Creamos el servicio
        var controller = new AccesoriosController(service); // 2. Le pasamos el servicio al controlador

        // Enviamos MarcaId = 99 y CategoriaId = 99 (ID que NO existen en la BD de prueba)
        var nuevoDto = new CrearAccesorioDto("Funda Falsa", 99, 99, 20m, 10);

        // Act
        var result = await controller.Crear(nuevoDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("No podrá registrarse un producto sin categoría ni marca válidas.", badRequestResult.Value);
    }

    [Fact]
    public async Task Crear_ConPrecioInvalido_RetornaBadRequest()
    {
        // Arrange
        var context = CrearContexto();
        var service = new AccesoriosService(context);       // 1. Creamos el servicio
        var controller = new AccesoriosController(service); // 2. Le pasamos el servicio al controlador

        // Enviamos precio 0 en el cuarto parámetro (20m -> 0m)
        var nuevoDto = new CrearAccesorioDto("Cable Genérico", 1, 1, 0m, 10);

        // Act
        var result = await controller.Crear(nuevoDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("El precio debe ser un valor mayor a cero.", badRequestResult.Value);
    }
}