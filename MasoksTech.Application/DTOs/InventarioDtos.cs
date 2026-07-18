namespace MasoksTech.Application.DTOs;

public record RegistrarVentaDto(int ProductoId, int Cantidad);

public record RespuestaVentaDto(string Mensaje, int StockResultante, bool AlertaStockMinimo);