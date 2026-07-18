using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.Infrastructure.Data;
using MasoksTech.Domain.Entities;
using MasoksTech.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MasoksTech.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

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
        
        var rolPredeterminado = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == "Cliente");

        var nuevoUsuario = new Usuario
        {
            Nombre = dto.Nombre,
            Correo = dto.Correo,
            Password = dto.Password, // Debería estar hasheado
            RolId = rolPredeterminado?.Id ?? 0
        };

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        var token = GenerarJwtToken(nuevoUsuario);

        return Ok(new RespuestaLoginDto("Usuario registrado con éxito.", token, nuevoUsuario.Nombre));
    }

    [HttpPost("login")]
    public async Task<ActionResult<RespuestaLoginDto>> Login(LoginUsuarioDto dto)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == dto.Correo && u.Password == dto.Password);
        if (usuario == null)
        {
            return Unauthorized("Credenciales incorrectas.");
        }

        var token = GenerarJwtToken(usuario);

        return Ok(new RespuestaLoginDto("Acceso concedido.", token, usuario.Nombre));
    }

    private string GenerarJwtToken(Usuario usuario)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? "ClaveSuperSecretaParaMasoksTech1234567890!");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Correo),
                new Claim(ClaimTypes.Name, usuario.Nombre)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}