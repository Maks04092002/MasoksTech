using Microsoft.EntityFrameworkCore;
using MasoksTech.Domain.Entities;

namespace MasoksTech.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Permiso> Permisos => Set<Permiso>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Marca> Marcas => Set<Marca>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Inventario> Inventarios => Set<Inventario>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Carrito> Carritos => Set<Carrito>();
    public DbSet<DetalleCarrito> DetallesCarrito => Set<DetalleCarrito>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<Envio> Envios => Set<Envio>();
    public DbSet<Bitacora> Bitacoras => Set<Bitacora>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optional: Fluent API configuration for constraints/relations
        // Example: ensuring unique emails
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Correo)
            .IsUnique();
    }
}
