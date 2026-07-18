using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.Infrastructure.Data;
using MasoksTech.Domain.Entities;
using MasoksTech.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MasoksTech.API.Controllers;

[ApiController]
[Route("api/inventario")]
[Authorize]
public class InventarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public InventarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("venta")]
    public async Task<ActionResult<RespuestaVentaDto>> RegistrarVenta(RegistrarVentaDto dto)
    {
        var producto = await _context.Productos.Include(p => p.Inventario).FirstOrDefaultAsync(p => p.Id == dto.ProductoId);
        if (producto == null)
        {
            return NotFound(new { Mensaje = "El producto solicitado no existe." });
        }

        if (producto.Inventario == null)
        {
            producto.Inventario = new Inventario { ProductoId = producto.Id, Stock = 0 };
            _context.Inventarios.Add(producto.Inventario);
        }

        if (producto.Inventario.Stock < dto.Cantidad || producto.Inventario.Stock == 0)
        {
            return BadRequest(new { Mensaje = "No se permitirá vender productos sin stock disponible" });
        }

        producto.Inventario.Stock -= dto.Cantidad;

        var bitacora = new Bitacora
        {
            Accion = $"Venta de {dto.Cantidad} unidades de {producto.Nombre}",
            Modulo = "Inventario",
            Fecha = DateTime.UtcNow
        };
        _context.Bitacoras.Add(bitacora);

        await _context.SaveChangesAsync();

        bool dispararAlerta = producto.Inventario.Stock <= producto.Inventario.StockMinimo;
        string mensajeResultado = "Venta procesada exitosamente.";

        if (dispararAlerta)
        {
            mensajeResultado += " ALERTA: El producto ha alcanzado o cruzado el límite de stock mínimo.";
        }

        return Ok(new RespuestaVentaDto(mensajeResultado, producto.Inventario.Stock, dispararAlerta));
    }
}