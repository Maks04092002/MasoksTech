namespace MasoksTech.Application.DTOs;

public record RegistroUsuarioDto(string Nombre, string Correo, string Password);

public record LoginUsuarioDto(string Correo, string Password);

public record RespuestaLoginDto(string Mensaje, string Token, string UsuarioNombre);

public record ProcesarPagoDto(decimal Monto, string NumeroTarjeta, string MetodoPago);

public record RespuestaPagoDto(string TransaccionId, string Estado, string Mensaje);