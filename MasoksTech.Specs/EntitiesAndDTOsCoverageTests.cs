using System;
using Xunit;
using MasoksTech.Domain.Entities;
using MasoksTech.Application.DTOs;

namespace MasoksTech.Specs;

public class EntitiesAndDTOsCoverageTests
{
    [Fact]
    public void Test_All_Entities_Coverage()
    {
        // Rol
        var rol = new Rol { Id = 1, Nombre = "Admin" };
        Assert.Equal(1, rol.Id);
        Assert.Equal("Admin", rol.Nombre);
        Assert.NotNull(rol.Usuarios);
        Assert.NotNull(rol.Permisos);

        // Permiso
        var permiso = new Permiso { Id = 1, Nombre = "Read" };
        Assert.Equal(1, permiso.Id);
        Assert.Equal("Read", permiso.Nombre);
        Assert.NotNull(permiso.Roles);

        // Usuario
        var usuario = new Usuario { Id = 1, Nombre = "User", Correo = "a@a.com", Password = "123", RolId = 1, Rol = rol };
        Assert.Equal(1, usuario.Id);
        Assert.Equal("User", usuario.Nombre);
        Assert.Equal("a@a.com", usuario.Correo);
        Assert.Equal("123", usuario.Password);
        Assert.Equal(1, usuario.RolId);
        Assert.NotNull(usuario.Rol);

        // Categoria
        var categoria = new Categoria { Id = 1, Nombre = "Cat" };
        Assert.Equal(1, categoria.Id);
        Assert.NotNull(categoria.Productos);

        // Marca
        var marca = new Marca { Id = 1, Nombre = "Marca" };
        Assert.Equal(1, marca.Id);
        Assert.NotNull(marca.Productos);

        // Inventario
        var inventario = new Inventario { Id = 1, Stock = 10, StockMinimo = 5, ProductoId = 1, Producto = new Producto() };
        Assert.Equal(1, inventario.Id);
        Assert.Equal(10, inventario.Stock);
        Assert.Equal(5, inventario.StockMinimo);
        Assert.Equal(1, inventario.ProductoId);
        Assert.NotNull(inventario.Producto);

        // Producto
        var producto = new Producto { Id = 1, Nombre = "Prod", Precio = 10.5m, CategoriaId = 1, Categoria = categoria, MarcaId = 1, Marca = marca, Inventario = inventario };
        Assert.Equal(1, producto.Id);
        Assert.Equal("Prod", producto.Nombre);
        Assert.Equal(10.5m, producto.Precio);
        Assert.NotNull(producto.Categoria);
        Assert.NotNull(producto.Marca);
        Assert.NotNull(producto.Inventario);

        // Cliente
        var cliente = new Cliente { Id = 1, Nombre = "Cli", Correo = "c@c.com", Telefono = "123", Direccion = "Dir" };
        Assert.Equal(1, cliente.Id);
        Assert.Equal("Cli", cliente.Nombre);
        Assert.Equal("c@c.com", cliente.Correo);
        Assert.Equal("123", cliente.Telefono);
        Assert.Equal("Dir", cliente.Direccion);

        // Carrito
        var carrito = new Carrito { Id = 1, ClienteId = 1, Cliente = cliente };
        Assert.Equal(1, carrito.Id);
        Assert.Equal(1, carrito.ClienteId);
        Assert.NotNull(carrito.Cliente);
        Assert.NotNull(carrito.Detalles);

        // DetalleCarrito
        var detCarrito = new DetalleCarrito { Id = 1, Cantidad = 2, PrecioUnitario = 10, CarritoId = 1, Carrito = carrito, ProductoId = 1, Producto = producto };
        Assert.Equal(1, detCarrito.Id);
        Assert.Equal(2, detCarrito.Cantidad);
        Assert.Equal(10, detCarrito.PrecioUnitario);
        Assert.NotNull(detCarrito.Carrito);
        Assert.NotNull(detCarrito.Producto);

        // Pago
        var pago = new Pago { Id = 1, Monto = 100, Fecha = DateTime.UtcNow, Metodo = "T", PedidoId = 1, Pedido = new Pedido() };
        Assert.Equal(1, pago.Id);
        Assert.Equal(100, pago.Monto);
        Assert.Equal("T", pago.Metodo);
        Assert.NotNull(pago.Pedido);

        // Envio
        var envio = new Envio { Id = 1, Direccion = "Dir", Estado = "E", NumeroSeguimiento = "123", PedidoId = 1, Pedido = new Pedido() };
        Assert.Equal(1, envio.Id);
        Assert.Equal("Dir", envio.Direccion);
        Assert.Equal("E", envio.Estado);
        Assert.Equal("123", envio.NumeroSeguimiento);
        Assert.NotNull(envio.Pedido);

        // Pedido
        var pedido = new Pedido { Id = 1, Fecha = DateTime.UtcNow, Total = 100, Estado = "P", ClienteId = 1, Cliente = cliente, Pago = pago, Envio = envio };
        Assert.Equal(1, pedido.Id);
        Assert.Equal(100, pedido.Total);
        Assert.Equal("P", pedido.Estado);
        Assert.NotNull(pedido.Cliente);
        Assert.NotNull(pedido.Pago);
        Assert.NotNull(pedido.Envio);
        Assert.NotNull(pedido.Detalles);

        // DetallePedido
        var detPedido = new DetallePedido { Id = 1, Cantidad = 2, PrecioUnitario = 10, PedidoId = 1, Pedido = pedido, ProductoId = 1, Producto = producto };
        Assert.Equal(1, detPedido.Id);
        Assert.Equal(2, detPedido.Cantidad);
        Assert.Equal(10, detPedido.PrecioUnitario);
        Assert.NotNull(detPedido.Pedido);
        Assert.NotNull(detPedido.Producto);

        // Bitacora
        var bitacora = new Bitacora { Id = 1, Fecha = DateTime.UtcNow, Accion = "A", Modulo = "M", UsuarioId = 1, Usuario = usuario };
        Assert.Equal(1, bitacora.Id);
        Assert.Equal("A", bitacora.Accion);
        Assert.Equal("M", bitacora.Modulo);
        Assert.Equal(1, bitacora.UsuarioId);
        Assert.NotNull(bitacora.Usuario);
    }

    [Fact]
    public void Test_All_DTOs_Coverage()
    {
        var regDto = new RegistroUsuarioDto("N", "C", "P");
        Assert.Equal("N", regDto.Nombre);
        
        var loginDto = new LoginUsuarioDto("C", "P");
        Assert.Equal("C", loginDto.Correo);

        var resLogin = new RespuestaLoginDto("M", "T", "U");
        Assert.Equal("M", resLogin.Mensaje);

        var procPago = new ProcesarPagoDto(100, "1", "T");
        Assert.Equal(100, procPago.Monto);

        var resPago = new RespuestaPagoDto("T", "E", "M");
        Assert.Equal("T", resPago.TransaccionId);


        var regVenta = new RegistrarVentaDto(1, 2);
        Assert.Equal(1, regVenta.ProductoId);

        var resVenta = new RespuestaVentaDto("M", 10, false);
        Assert.Equal("M", resVenta.Mensaje);

        var detPed = new DetallePedidoDto(1, 2);
        Assert.Equal(1, detPed.ProductoId);

        var crearPed = new CrearPedidoDto("N", "C", "T", "D", new System.Collections.Generic.List<DetallePedidoDto>());
        Assert.Equal("N", crearPed.ClienteNombre);

        var resPed = new RespuestaPedidoDto("M", 1, 100);
        Assert.Equal("M", resPed.Mensaje);
    }
}
