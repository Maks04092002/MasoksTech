namespace MasoksTech.API.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class Marca
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class Accesorio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int MarcaId { get; set; }
    public int CategoriaId { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public int StockMinimo { get; set; } = 5;

    public Marca? Marca { get; set; }
    public Categoria? Categoria { get; set; }
}

public class MovimientoInventario
{
    public int Id { get; set; }
    public int AccesorioId { get; set; }
    public int Cantidad { get; set; }
    public string Tipo { get; set; } = "Salida";
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string Detalle { get; set; } = string.Empty;

    public Accesorio? Accesorio { get; set; }
}