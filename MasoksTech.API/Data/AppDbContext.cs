using Microsoft.EntityFrameworkCore;
using MasoksTech.API.Models;

namespace MasoksTech.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Accesorio> Accesorios => Set<Accesorio>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Marca> Marcas => Set<Marca>();
    public DbSet<MovimientoInventario> Movimientos => Set<MovimientoInventario>();
}