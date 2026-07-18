namespace MasoksTech.Application.DTOs;

public record CrearPedidoDto(
    string ClienteNombre,
    string ClienteCorreo,
    string ClienteTelefono,
    string DireccionEnvio,
    List<DetallePedidoDto> Detalles
);

public record DetallePedidoDto(
    int ProductoId,
    int Cantidad
);

public record RespuestaPedidoDto(
    string Mensaje,
    int PedidoId,
    decimal Total
);
