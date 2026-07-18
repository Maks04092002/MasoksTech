using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.Infrastructure.Data;
using MasoksTech.Domain.Entities;
using MasoksTech.Application.DTOs;
using MasoksTech.API.Controllers;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace MasoksTech.Specs;

public class AuthControllerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);

        context.Roles.Add(new Rol { Id = 1, Nombre = "Administrador" });
        context.Roles.Add(new Rol { Id = 2, Nombre = "Cliente" });
        context.SaveChanges();

        return context;
    }

    private IConfiguration GetConfiguration()
    {
        var myConfiguration = new Dictionary<string, string?>
        {
            {"Jwt:Key", "ClaveSuperSecretaParaMasoksTech1234567890!"}
        };
        return new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenValidData()
    {
        using var context = GetDbContext();
        var controller = new AuthController(context, GetConfiguration());
        var dto = new RegistroUsuarioDto("test", "test@test.com", "pass123");

        var result = await controller.Registrar(dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Contains("registrado con éxito", okResult.Value!.ToString());
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenUserExists()
    {
        using var context = GetDbContext();
        var controller = new AuthController(context, GetConfiguration());
        
        context.Usuarios.Add(new Usuario { Nombre = "test", Correo = "test@test.com", Password = "hash", RolId = 2 });
        context.SaveChanges();

        var dto = new RegistroUsuarioDto("test2", "test@test.com", "pass123");
        var result = await controller.Registrar(dto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Contains("correo ya se encuentra registrado", badRequestResult.Value!.ToString());
    }

    [Fact]
    public async Task Login_ReturnsOkWithToken_WhenValidCredentials()
    {
        using var context = GetDbContext();
        var controller = new AuthController(context, GetConfiguration());

        var dtoReg = new RegistroUsuarioDto("test", "test@test.com", "pass123");
        await controller.Registrar(dtoReg);

        var dtoLogin = new LoginUsuarioDto("test@test.com", "pass123");
        var result = await controller.Login(dtoLogin);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var resDto = Assert.IsType<RespuestaLoginDto>(okResult.Value);
        Assert.NotNull(resDto.Token);
    }
}
