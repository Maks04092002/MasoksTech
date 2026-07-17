namespace MasoksTech.API.DTOs;

// DTOs para el sistema de Login y Registro
public record RegistroUsuarioDto(string Nombre, string Correo, string Password);
public record LoginUsuarioDto(string Correo, string Password);
public record RespuestaLoginDto(string Mensaje, string Token, string Nombre);

// DTOs para la simulación de la Pasarela de Pagos
public record ProcesarPagoDto(decimal Monto, string MetodoPago, string NumeroTarjeta);
public record RespuestaPagoDto(string TransaccionId, string Estado, string Mensaje);