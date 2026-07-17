using MasoksTech.API.DTOs;

namespace MasoksTech.API.Interfaces;

public interface IAccesoriosService
{
    Task<IEnumerable<AccesorioResponseDto>> ObtenerTodosAsync();
    Task<AccesorioResponseDto> CrearAsync(CrearAccesorioDto dto);
}