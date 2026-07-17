using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.API.Data;
using MasoksTech.API.Models;
using MasoksTech.API.DTOs;

namespace MasoksTech.API.Controllers;

[ApiController]
[Route("api/inventario")]
public class InventarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public InventarioController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/inventario/venta
    [HttpPost("venta")]
    public async Task<ActionResult<RespuestaVentaDto>> RegistrarVenta(RegistrarVentaDto dto)
    {
        // 1. Buscar si el accesorio existe en la base de datos
        var accesorio = await _context.Accesorios.FindAsync(dto.AccesorioId);
        if (accesorio == null)
        {
            return NotFound(new { Mensaje = "El accesorio solicitado no existe." });
        }

        // 2. Regla de Negocio: Impedir la venta si no hay stock suficiente
        if (accesorio.Stock < dto.Cantidad || accesorio.Stock == 0)
        {
            return BadRequest(new { Mensaje = "No se permitirá vender productos sin stock disponible" });
        }

        // 3. Modificar el stock físico del producto
        accesorio.Stock -= dto.Cantidad;

        // 4. Registrar la auditoría del movimiento en el almacén
        var movimiento = new MovimientoInventario
        {
            AccesorioId = dto.AccesorioId,
            Cantidad = dto.Cantidad,
            Tipo = "Salida",
            Fecha = DateTime.UtcNow,
            Detalle = $"Venta procesada de {dto.Cantidad} unidades."
        };

        _context.Movimientos.Add(movimiento);

        // Confirmar cambios en la base de datos SQLite / In-Memory
        await _context.SaveChangesAsync();

        // 5. Regla de Negocio: Evaluar si se dispara la alerta de Stock Mínimo
        bool dispararAlerta = accesorio.Stock <= accesorio.StockMinimo;
        string mensajeResultado = "Venta procesada exitosamente.";

        if (dispararAlerta)
        {
            mensajeResultado += " ALERTA: El producto ha alcanzado o cruzado el límite de stock mínimo.";
        }

        var respuesta = new RespuestaVentaDto(
            Mensaje: mensajeResultado,
            StockResultante: accesorio.Stock,
            AlertaStockMinimo: dispararAlerta
        );

        return Ok(respuesta);
    }
}