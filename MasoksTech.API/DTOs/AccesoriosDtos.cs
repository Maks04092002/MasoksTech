namespace MasoksTech.API.DTOs;

// Para el registro de accesorios
public record CrearAccesorioDto(string Nombre, int MarcaId, int CategoriaId, decimal Precio, int StockInicial);

// Para las respuestas del catálogo
public record AccesorioResponseDto(int Id, string Nombre, string Marca, string Categoria, decimal Precio, int Stock);

// Para procesar una venta
public record RegistrarVentaDto(int AccesorioId, int Cantidad);

// Para la respuesta de una venta (incluye posibles alertas)
public record RespuestaVentaDto(string Mensaje, int StockResultante, bool AlertaStockMinimo);