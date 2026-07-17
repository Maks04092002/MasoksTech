using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.API.Controllers;
using MasoksTech.API.Data;
using MasoksTech.API.Models;
using MasoksTech.API.DTOs;

namespace MasoksTech.Specs;

public class AuthControllerTests
{
    private AppDbContext CrearContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Registrar_Exitoso_RetornaOk()
    {
        var context = CrearContexto();
        var controller = new AuthController(context);
        var dto = new RegistroUsuarioDto("Maks", "maks@test.com", "password123");

        var result = await controller.Registrar(dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var respuesta = Assert.IsType<RespuestaLoginDto>(okResult.Value);
        Assert.Equal("Maks", respuesta.Nombre);
    }

    [Fact]
    public async Task Registrar_DatosVacios_RetornaBadRequest()
    {
        var context = CrearContexto();
        var controller = new AuthController(context);
        var dto = new RegistroUsuarioDto("", "", "");

        var result = await controller.Registrar(dto);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task Registrar_CorreoDuplicado_RetornaBadRequest()
    {
        var context = CrearContexto();
        context.Usuarios.Add(new Usuario { Nombre = "Maks Original", Correo = "maks@test.com", Password = "123" });
        context.SaveChanges();

        var controller = new AuthController(context);
        var dto = new RegistroUsuarioDto("Maks Clon", "maks@test.com", "456");

        var result = await controller.Registrar(dto);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task Login_Exitoso_RetornaOk()
    {
        var context = CrearContexto();
        context.Usuarios.Add(new Usuario { Nombre = "Maks", Correo = "maks@test.com", Password = "123" });
        context.SaveChanges();

        var controller = new AuthController(context);
        var dto = new LoginUsuarioDto("maks@test.com", "123");

        var result = await controller.Login(dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var respuesta = Assert.IsType<RespuestaLoginDto>(okResult.Value);
        Assert.Equal("Maks", respuesta.Nombre);
    }

    [Fact]
    public async Task Login_CredencialesIncorrectas_RetornaUnauthorized()
    {
        var context = CrearContexto();
        var controller = new AuthController(context);
        var dto = new LoginUsuarioDto("maks@test.com", "password_incorrecto");

        var result = await controller.Login(dto);

        Assert.IsType<UnauthorizedObjectResult>(result.Result);
    }
}