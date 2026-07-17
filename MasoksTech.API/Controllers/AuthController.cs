using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.API.Data;
using MasoksTech.API.Models;
using MasoksTech.API.DTOs;

namespace MasoksTech.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/auth/registro
    [HttpPost("registro")]
    public async Task<ActionResult<RespuestaLoginDto>> Registrar(RegistroUsuarioDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Correo) || string.IsNullOrWhiteSpace(dto.Password))
        {
            return BadRequest("El correo y la contraseña son obligatorios.");
        }

        var existe = await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo);
        if (existe)
        {
            return BadRequest("El correo ya se encuentra registrado.");
        }

        var nuevoUsuario = new Usuario
        {
            Nombre = dto.Nombre,
            Correo = dto.Correo,
            Password = dto.Password
        };

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        return Ok(new RespuestaLoginDto("Usuario registrado con éxito.", "TOKEN-JWT-MOCK-12345", nuevoUsuario.Nombre));
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<ActionResult<RespuestaLoginDto>> Login(LoginUsuarioDto dto)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == dto.Correo && u.Password == dto.Password);
        if (usuario == null)
        {
            return Unauthorized("Credenciales incorrectas.");
        }

        return Ok(new RespuestaLoginDto("Acceso concedido.", "TOKEN-JWT-MOCK-12345", usuario.Nombre));
    }
}