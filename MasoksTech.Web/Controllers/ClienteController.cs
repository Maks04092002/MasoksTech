using Microsoft.AspNetCore.Mvc;
using MasoksTech.Web.Data;
using MasoksTech.Web.Models;
using System;
using System.Linq;

namespace MasoksTech.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos el contexto real de SQLite
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos.ToList();
            return View(productos);
        }

        [HttpPost]
        public IActionResult ProcesarPago(int productoId, string nombreTarjeta, string numeroTarjeta, string expiracion, string cvv)
        {
            // 1. Buscar el producto real en SQLite
            var producto = _context.Productos.FirstOrDefault(p => p.Id == productoId);

            if (producto == null)
            {
                TempData["CompraStatusError"] = "El producto seleccionado ya no está disponible.";
                return RedirectToAction("Index");
            }

            if (producto.Stock <= 0)
            {
                TempData["CompraStatusError"] = $"Lo sentimos, '{producto.Nombre}' no cuenta con stock disponible.";
                return RedirectToAction("Index");
            }

            // Validaciones de tarjeta de la pasarela
            if (string.IsNullOrWhiteSpace(numeroTarjeta) || numeroTarjeta.Replace(" ", "").Length < 16)
            {
                TempData["CompraStatusError"] = "Transacción Rechazada: El número de tarjeta debe tener 16 dígitos.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(cvv) || cvv.Length < 3)
            {
                TempData["CompraStatusError"] = "Transacción Rechazada: Código CVV inválido.";
                return RedirectToAction("Index");
            }

            // 2. Modificar los datos en el contexto
            producto.Stock--; // Restamos 1 al stock físico en SQLite

            var nuevaVenta = new VentaItem
            {
                ClienteCorreo = "cliente.activo@masokstech.com",
                ProductoNombre = producto.Nombre,
                Total = producto.Precio,
                Fecha = DateTime.Now,
                Estado = "Aprobado"
            };

            _context.Ventas.Add(nuevaVenta); // Agrega la boleta de venta a SQLite

            // 3. 🌟 EL PASO CRÍTICO: Guardar todo físicamente en el archivo masokstech.db
            _context.SaveChanges();

            TempData["CompraStatus"] = $"🎉 ¡PAGO APROBADO CON ÉXITO! Se cargaron S/ {producto.Precio:F2} a tu tarjeta. Adquiriste: '{producto.Nombre}'.";
            return RedirectToAction("Index");
        }
    }
}