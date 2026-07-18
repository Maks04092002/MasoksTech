using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.Infrastructure.Data;
using MasoksTech.Domain.Entities;
using MasoksTech.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MasoksTech.API.Controllers;

[ApiController]
[Route("api/pedidos")]
[Authorize]
public class PedidosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PedidosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<RespuestaPedidoDto>> RegistrarPedido(CrearPedidoDto dto)
    {
        if (dto.Detalles == null || !dto.Detalles.Any())
        {
            return BadRequest(new { Mensaje = "El pedido debe contener al menos un producto." });
        }

        // 1. Buscar o crear cliente
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == dto.ClienteCorreo);
        if (cliente == null)
        {
            cliente = new Cliente
            {
                Nombre = dto.ClienteNombre,
                Correo = dto.ClienteCorreo,
                Telefono = dto.ClienteTelefono,
                Direccion = dto.DireccionEnvio
            };
            _context.Clientes.Add(cliente);
        }

        var pedido = new Pedido
        {
            Cliente = cliente,
            Fecha = DateTime.UtcNow,
            Estado = "Pendiente",
            Envio = new Envio { Direccion = dto.DireccionEnvio }
        };

        decimal total = 0;

        foreach (var detalle in dto.Detalles)
        {
            var producto = await _context.Productos.Include(p => p.Inventario).FirstOrDefaultAsync(p => p.Id == detalle.ProductoId);
            if (producto == null)
            {
                return NotFound(new { Mensaje = $"El producto con ID {detalle.ProductoId} no existe." });
            }

            if (producto.Inventario == null || producto.Inventario.Stock < detalle.Cantidad)
            {
                return BadRequest(new { Mensaje = $"No hay stock suficiente para el producto: {producto.Nombre}." });
            }

            // Disminuir inventario
            producto.Inventario.Stock -= detalle.Cantidad;

            // Registrar movimiento en bitacora
            _context.Bitacoras.Add(new Bitacora
            {
                Accion = $"Venta de {detalle.Cantidad} unidades de {producto.Nombre} (Pedido)",
                Modulo = "Pedidos",
                Fecha = DateTime.UtcNow
            });

            var subtotal = producto.Precio * detalle.Cantidad;
            total += subtotal;

            pedido.Detalles.Add(new DetallePedido
            {
                ProductoId = producto.Id,
                Cantidad = detalle.Cantidad,
                PrecioUnitario = producto.Precio
            });
        }

        pedido.Total = total;
        _context.Pedidos.Add(pedido);

        await _context.SaveChangesAsync();

        return Ok(new RespuestaPedidoDto("Pedido registrado con éxito.", pedido.Id, pedido.Total));
    }
}
