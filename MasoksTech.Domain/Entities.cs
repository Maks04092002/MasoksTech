namespace MasoksTech.Domain.Entities;

public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}

public class Permiso
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Rol> Roles { get; set; } = new List<Rol>();
}

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public int RolId { get; set; }
    public Rol? Rol { get; set; }
}

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

public class Marca
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    
    public int MarcaId { get; set; }
    public Marca? Marca { get; set; }
    
    public Inventario? Inventario { get; set; }
}

public class Inventario
{
    public int Id { get; set; }
    public int Stock { get; set; }
    public int StockMinimo { get; set; } = 5;
    
    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }
}

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
}

public class Carrito
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    
    public ICollection<DetalleCarrito> Detalles { get; set; } = new List<DetalleCarrito>();
}

public class DetalleCarrito
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    
    public int CarritoId { get; set; }
    public Carrito? Carrito { get; set; }
    
    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }
}

public class Pedido
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public string Estado { get; set; } = "Pendiente";
    
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    
    public Pago? Pago { get; set; }
    public Envio? Envio { get; set; }
    public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
}

public class DetallePedido
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    
    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
    
    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }
}

public class Pago
{
    public int Id { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string Metodo { get; set; } = string.Empty;
    
    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
}

public class Envio
{
    public int Id { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string Estado { get; set; } = "En Proceso";
    public string NumeroSeguimiento { get; set; } = string.Empty;
    
    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
}

public class Bitacora
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string Accion { get; set; } = string.Empty;
    public string Modulo { get; set; } = string.Empty;
    
    public int? UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}
