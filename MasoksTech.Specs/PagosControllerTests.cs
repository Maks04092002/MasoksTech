using Microsoft.AspNetCore.Mvc;
using MasoksTech.Application.DTOs;
using MasoksTech.API.Controllers;
using Xunit;

namespace MasoksTech.Specs;

public class PagosControllerTests
{
    [Fact]
    public void ProcesarPago_ReturnsOk_WhenValidData()
    {
        var controller = new PagosController();
        var dto = new ProcesarPagoDto(100, "1234567890123456", "Tarjeta");

        var result = controller.ProcesarPago(dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var resDto = Assert.IsType<RespuestaPagoDto>(okResult.Value);
        
        Assert.Equal("APROBADO", resDto.Estado);
    }

    [Fact]
    public void ProcesarPago_ReturnsBadRequest_WhenMontoInsuficiente()
    {
        var controller = new PagosController();
        var dto = new ProcesarPagoDto(0, "1234567890123456", "Tarjeta");

        var result = controller.ProcesarPago(dto);

        var badResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var resDto = Assert.IsType<RespuestaPagoDto>(badResult.Value);
        
        Assert.Equal("RECHAZADO", resDto.Estado);
    }
}
