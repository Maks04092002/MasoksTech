using Microsoft.AspNetCore.Mvc;
using MasoksTech.API.DTOs;

namespace MasoksTech.API.Controllers;

[ApiController]
[Route("api/pagos")]
public class PagosController : ControllerBase
{
    // POST: api/pagos/procesar
    [HttpPost("procesar")]
    public ActionResult<RespuestaPagoDto> ProcesarPago(ProcesarPagoDto dto)
    {
        // 1. Regla de negocio: El monto debe ser válido
        if (dto.Monto <= 0)
        {
            return BadRequest(new RespuestaPagoDto("", "RECHAZADO", "El monto a pagar debe ser mayor a cero."));
        }

        // 2. Regla de negocio: Validar que la tarjeta tenga 16 dígitos
        if (string.IsNullOrWhiteSpace(dto.NumeroTarjeta) || dto.NumeroTarjeta.Length != 16)
        {
            return BadRequest(new RespuestaPagoDto("", "RECHAZADO", "El número de tarjeta debe tener exactamente 16 dígitos."));
        }

        // 3. Simulación de transacción exitosa (Pasarela aprobada)
        var transaccionId = "TXN-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        return Ok(new RespuestaPagoDto(
            transaccionId,
            "APROBADO",
            $"Pago procesado exitosamente mediante {dto.MetodoPago}."
        ));
    }
}