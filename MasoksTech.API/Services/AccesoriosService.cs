using Microsoft.EntityFrameworkCore;
using MasoksTech.API.Data;
using MasoksTech.API.Models;
using MasoksTech.API.DTOs;
using MasoksTech.API.Interfaces;

namespace MasoksTech.API.Services;

public class AccesoriosService : IAccesoriosService
{
    private readonly AppDbContext _context;

    public AccesoriosService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccesorioResponseDto>> ObtenerTodosAsync()
    {
        return await _context.Accesorios
            .Include(a => a.Marca)
            .Include(a => a.Categoria)
            .Select(a => new AccesorioResponseDto(
                a.Id,
                a.Nombre,
                a.Marca != null ? a.Marca.Nombre : "Sin Marca",
                a.Categoria != null ? a.Categoria.Nombre : "Sin Categoría",
                a.Precio,
                a.Stock
            ))
            .ToListAsync();
    }

    public async Task<AccesorioResponseDto> CrearAsync(CrearAccesorioDto dto)
    {
        // Verificar que existan la marca y categoría
        var marcaExiste = await _context.Marcas.AnyAsync(m => m.Id == dto.MarcaId);
        var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);

        if (!marcaExiste || !categoriaExiste)
        {
            throw new ArgumentException("No podrá registrarse un producto sin categoría ni marca válidas.");
        }

        var nuevoAccesorio = new Accesorio
        {
            Nombre = dto.Nombre,
            MarcaId = dto.MarcaId,
            CategoriaId = dto.CategoriaId,
            Precio = dto.Precio,
            Stock = dto.StockInicial
        };

        _context.Accesorios.Add(nuevoAccesorio);
        await _context.SaveChangesAsync();

        var marca = await _context.Marcas.FindAsync(dto.MarcaId);
        var categoria = await _context.Categorias.FindAsync(dto.CategoriaId);

        return new AccesorioResponseDto(
            nuevoAccesorio.Id, nuevoAccesorio.Nombre, marca!.Nombre, categoria!.Nombre, nuevoAccesorio.Precio, nuevoAccesorio.Stock
        );
    }
}