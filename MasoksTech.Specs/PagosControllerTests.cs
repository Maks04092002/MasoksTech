using Microsoft.AspNetCore.Mvc;
using MasoksTech.API.Controllers;
using MasoksTech.API.DTOs;

namespace MasoksTech.Specs;

public class PagosControllerTests
{
    [Fact]
    public void ProcesarPago_Exitoso_RetornaOk()
    {
        var controller = new PagosController();
        var dto = new ProcesarPagoDto(150.00m, "Tarjeta de Crédito", "1234567812345678");

        var result = controller.ProcesarPago(dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var respuesta = Assert.IsType<RespuestaPagoDto>(okResult.Value);
        Assert.Equal("APROBADO", respuesta.Estado);
        Assert.NotEmpty(respuesta.TransaccionId);
    }

    [Fact]
    public void ProcesarPago_MontoInvalido_RetornaBadRequest()
    {
        var controller = new PagosController();
        var dto = new ProcesarPagoDto(-5.00m, "Tarjeta", "1234567812345678");

        var result = controller.ProcesarPago(dto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var respuesta = Assert.IsType<RespuestaPagoDto>(badRequestResult.Value);
        Assert.Equal("RECHAZADO", respuesta.Estado);
    }

    [Fact]
    public void ProcesarPago_TarjetaInvalida_RetornaBadRequest()
    {
        var controller = new PagosController();
        var dto = new ProcesarPagoDto(50.00m, "Tarjeta", "1234"); // Tarjeta corta inválida

        var result = controller.ProcesarPago(dto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var respuesta = Assert.IsType<RespuestaPagoDto>(badRequestResult.Value);
        Assert.Equal("RECHAZADO", respuesta.Estado);
    }
}