using Microsoft.AspNetCore.Mvc;
using MasoksTech.Web.Data;
using MasoksTech.Web.Models;
using System.Linq;

namespace MasoksTech.Web.Controllers
{
    public class InventarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos el contexto real de la base de datos (SQLite)
        public InventarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos.ToList();
            return View(productos);
        }

        [HttpPost]
        public IActionResult Agregar(string nombre, string categoria, int stock, decimal precio, string imagenUrl)
        {
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                // Si no introduce una imagen, usamos una por defecto
                string imagenFinal = string.IsNullOrWhiteSpace(imagenUrl)
                    ? "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=600&q=80"
                    : imagenUrl;

                var nuevoProducto = new InventarioItem
                {
                    Nombre = nombre,
                    Categoria = categoria,
                    Stock = stock,
                    Precio = precio,
                    ImagenUrl = imagenFinal
                };

                _context.Productos.Add(nuevoProducto);
                _context.SaveChanges(); // Guarda de manera permanente en SQLite
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges(); // Guarda la eliminación permanentemente
            }
            return RedirectToAction("Index");
        }
    }
}