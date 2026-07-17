using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MasoksTech.Web.Data;
using MasoksTech.Web.Models;

namespace MasoksTech.Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Cambiado de _context.Inventario a _context.Productos
            var inventario = _context.Productos.ToList();
            return View(inventario);
        }

        [HttpPost]
        public IActionResult Crear(InventarioItem nuevoProducto)
        {
            if (ModelState.IsValid)
            {
                // Cambiado de _context.Inventario a _context.Productos
                _context.Productos.Add(nuevoProducto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nuevoProducto);
        }
    }
}