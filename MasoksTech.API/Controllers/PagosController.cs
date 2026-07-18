using Microsoft.AspNetCore.Mvc;
using MasoksTech.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MasoksTech.API.Controllers;

[ApiController]
[Route("api/pagos")]
[Authorize]
public class PagosController : ControllerBase
{
    [HttpPost("procesar")]
    public ActionResult<RespuestaPagoDto> ProcesarPago(ProcesarPagoDto dto)
    {
        if (dto.Monto <= 0)
        {
            return BadRequest(new RespuestaPagoDto("", "RECHAZADO", "El monto a pagar debe ser mayor a cero."));
        }

        if (string.IsNullOrWhiteSpace(dto.NumeroTarjeta) || dto.NumeroTarjeta.Length != 16)
        {
            return BadRequest(new RespuestaPagoDto("", "RECHAZADO", "El número de tarjeta debe tener exactamente 16 dígitos."));
        }

        var transaccionId = "TXN-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        return Ok(new RespuestaPagoDto(
            transaccionId,
            "APROBADO",
            $"Pago procesado exitosamente mediante {dto.MetodoPago}."
        ));
    }
}