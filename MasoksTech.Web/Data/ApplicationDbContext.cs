using Microsoft.EntityFrameworkCore;
using MasoksTech.Web.Models;

namespace MasoksTech.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<InventarioItem> Productos { get; set; } = null!;
        public DbSet<UsuarioItem> Usuarios { get; set; } = null!;
        public DbSet<VentaItem> Ventas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Semilla de datos para inicializar la base de datos si está vacía
            modelBuilder.Entity<InventarioItem>().HasData(
                new InventarioItem
                {
                    Id = 1,
                    Nombre = "Teclado Mecánico Masoks G1",
                    Categoria = "Periféricos",
                    Stock = 15,
                    Precio = 320.00m,
                    ImagenUrl = "https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=600&q=80"
                },
                new InventarioItem
                {
                    Id = 2,
                    Nombre = "Mouse Gamer Masoks Pro",
                    Categoria = "Periféricos",
                    Stock = 20,
                    Precio = 180.00m,
                    ImagenUrl = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=600&q=80"
                },
                new InventarioItem
                {
                    Id = 3,
                    Nombre = "Audífonos Masoks H2 Stereo",
                    Categoria = "Audio",
                    Stock = 12,
                    Precio = 240.00m,
                    ImagenUrl = "https://images.unsplash.com/photo-1546435770-a3e426bf472b?w=600&q=80"
                }
            );

            modelBuilder.Entity<UsuarioItem>().HasData(
                new UsuarioItem { Id = 1, Nombre = "Maks Admin", Correo = "admin@masokstech.com", Rol = "Administrador", Activo = true },
                new UsuarioItem { Id = 2, Nombre = "Juan Pérez", Correo = "juan.perez@masokstech.com", Rol = "Vendedor", Activo = true }
            );
        }
    }
}