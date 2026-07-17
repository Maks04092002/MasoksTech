using Microsoft.AspNetCore.Mvc;
using MasoksTech.Web.Models;

namespace MasoksTech.Web.Controllers;

public class InventarioController : Controller
{
    public IActionResult Index()
    {
        return View(MockRepository.Productos);
    }

    [HttpPost]
    public IActionResult Agregar(string nombre, string categoria, int stock, decimal precio)
    {
        var nuevoId = MockRepository.Productos.Any() ? MockRepository.Productos.Max(p => p.Id) + 1 : 1;
        MockRepository.Productos.Add(new InventarioItem
        {
            Id = nuevoId,
            Nombre = nombre,
            Categoria = categoria,
            Stock = stock,
            Precio = precio
        });
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        var producto = MockRepository.Productos.FirstOrDefault(p => p.Id == id);
        if (producto != null)
        {
            MockRepository.Productos.Remove(producto);
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult Agregar(string nombre, string categoria, int stock, decimal precio, string imagenUrl)
    {
        var nuevoId = MockRepository.Productos.Any() ? MockRepository.Productos.Max(p => p.Id) + 1 : 1;

        // Si no introduce una imagen, usamos una por defecto de repuesto
        string imagenFinal = string.IsNullOrWhiteSpace(imagenUrl)
            ? "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=600&q=80"
            : imagenUrl;

        MockRepository.Productos.Add(new InventarioItem
        {
            Id = nuevoId,
            Nombre = nombre,
            Categoria = categoria,
            Stock = stock,
            Precio = precio,
            ImagenUrl = imagenFinal
        });
        return RedirectToAction("Index");
    }
}