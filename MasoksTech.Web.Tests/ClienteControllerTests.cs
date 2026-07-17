using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasoksTech.Web.Controllers;
using MasoksTech.Web.Data;
using MasoksTech.Web.Models;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MasoksTech.Web.Tests
{
    // 🌟 Simulador nativo de TempData para no requerir instalar "Moq"
    public class FakeTempDataProvider : ITempDataProvider
    {
        public IDictionary<string, object> LoadTempData(HttpContext context)
        {
            return new Dictionary<string, object>();
        }

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
        }
    }

    public class ClienteControllerTests
    {
        // Método auxiliar para crear una base de datos limpia en memoria RAM
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public void Index_DebeRetornarVistaConListaDeProductos()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.Productos.Add(new InventarioItem { Id = 1, Nombre = "Teclado de Prueba", Precio = 100m, Stock = 5 });
            context.SaveChanges();

            var controller = new ClienteController(context);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IEnumerable<InventarioItem>>(result.Model);
            Assert.Single(model);
        }

        [Fact]
        public void ProcesarPago_ConDatosValidos_DebeRestarStockYCrearVenta()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var producto = new InventarioItem { Id = 1, Nombre = "Mouse Gamer", Precio = 50m, Stock = 10 };
            context.Productos.Add(producto);
            context.SaveChanges();

            var controller = new ClienteController(context);

            // 🌟 Configuramos el TempData usando nuestro simulador nativo gratuito
            var httpContext = new DefaultHttpContext();
            var tempDataProvider = new FakeTempDataProvider();
            controller.TempData = new TempDataDictionary(httpContext, tempDataProvider);

            // Act: Simulamos la compra
            var result = controller.ProcesarPago(
                productoId: 1,
                nombreTarjeta: "Maks Admin",
                numeroTarjeta: "1234 5678 1234 5678",
                expiracion: "12/30",
                cvv: "123"
            ) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // Verificar que el stock bajó en la base de datos
            var productoActualizado = context.Productos.Find(1);
            Assert.Equal(9, productoActualizado.Stock);

            // Verificar que la venta se registró
            Assert.Single(context.Ventas);
            Assert.Equal("Mouse Gamer", context.Ventas.First().ProductoNombre);
        }
    }
}