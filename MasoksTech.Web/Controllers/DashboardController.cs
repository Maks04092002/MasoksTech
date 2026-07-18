using Microsoft.AspNetCore.Mvc;
using MasoksTech.Web.Data;
using MasoksTech.Web.Models;
using System.Linq;

namespace MasoksTech.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Construimos un ViewModel con los datos reales de la base de datos
            var viewModel = new DashboardViewModel
            {
                TotalProductos = _context.Productos.Count(),
                TotalUsuarios = _context.Usuarios.Count(),
                TotalVentas = _context.Ventas.Count(),
                StockTotal = _context.Productos.Sum(p => (int?)p.Stock) ?? 0,
                IngresosTotales = _context.Ventas.Sum(v => (decimal?)v.Total) ?? 0m,
                ProductosConBajoStock = _context.Productos.Where(p => p.Stock <= 5).ToList(),
                UltimasVentas = _context.Ventas.OrderByDescending(v => v.Fecha).Take(5).ToList()
            };

            return View(viewModel);
        }
    }
}