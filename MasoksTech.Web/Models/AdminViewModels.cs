namespace MasoksTech.Web.Models;

public class InventarioItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Precio { get; set; }
    public string ImagenUrl { get; set; } = string.Empty; // 🌟 NUEVO: URL de la imagen del producto
}

public class UsuarioItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public bool Activo { get; set; }
}

public class VentaItem
{
    public int Id { get; set; }
    public string ClienteCorreo { get; set; } = string.Empty;
    public string ProductoNombre { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; } = "Completado";
}

public static class MockRepository
{
    public static List<InventarioItem> Productos { get; set; } = new()
    {
        // 🌟 NUEVO: Hemos agregado imágenes espectaculares para tus 3 productos iniciales
        new() {
            Id = 1,
            Nombre = "Teclado Mecánico Masoks G1",
            Categoria = "Periféricos",
            Stock = 15,
            Precio = 320.00m,
            ImagenUrl = "https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=600&q=80"
        },
        new() {
            Id = 2,
            Nombre = "Mouse Gamer Masoks Pro",
            Categoria = "Periféricos",
            Stock = 20,
            Precio = 180.00m,
            ImagenUrl = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=600&q=80"
        },
        new() {
            Id = 3,
            Nombre = "Audífonos Masoks H2 Stereo",
            Categoria = "Audio",
            Stock = 12,
            Precio = 240.00m,
            ImagenUrl = "https://images.unsplash.com/photo-1546435770-a3e426bf472b?w=600&q=80"
        }
    };

    public static List<UsuarioItem> Usuarios { get; set; } = new()
    {
        new() { Id = 1, Nombre = "Maks Admin", Correo = "admin@masokstech.com", Rol = "Administrador", Activo = true },
        new() { Id = 2, Nombre = "Juan Pérez", Correo = "juan.perez@masokstech.com", Rol = "Vendedor", Activo = true }
    };

    public static List<VentaItem> Ventas { get; set; } = new()
    {
        new() { Id = 1, ClienteCorreo = "cliente.demo@gmail.com", ProductoNombre = "Teclado Mecánico Masoks G1", Total = 320.00m, Fecha = DateTime.Now.AddMinutes(-45) }
    };
}