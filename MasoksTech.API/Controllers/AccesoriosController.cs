using Microsoft.AspNetCore.Mvc;
using MasoksTech.API.DTOs;
using MasoksTech.API.Interfaces;

namespace MasoksTech.API.Controllers;

[ApiController]
[Route("api/accesorios")]
public class AccesoriosController : ControllerBase
{
    private readonly IAccesoriosService _accesoriosService;

    // Inyectamos la Interfaz, ya NO el AppDbContext
    public AccesoriosController(IAccesoriosService accesoriosService)
    {
        _accesoriosService = accesoriosService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccesorioResponseDto>>> ObtenerTodos()
    {
        var accesorios = await _accesoriosService.ObtenerTodosAsync();
        return Ok(accesorios);
    }

    [HttpPost]
    public async Task<ActionResult<AccesorioResponseDto>> Crear(CrearAccesorioDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre)) return BadRequest("El nombre del accesorio es obligatorio.");
        if (dto.Precio <= 0) return BadRequest("El precio debe ser un valor mayor a cero.");

        try
        {
            var respuesta = await _accesoriosService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerTodos), new { id = respuesta.Id }, respuesta);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}