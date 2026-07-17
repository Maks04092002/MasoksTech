📋 /speckit.tasks: BACKLOG DE TAREAS EJECUTABLES
Para este Sprint Inicial (Catálogo e Inventario), dividiremos el trabajo en 4 fases secuenciales:

📊 Fase 1: Configuración de Datos y Modelo Físico (API)
[ ] Tarea 1.1: Instalar mediante el Administrador de Paquetes NuGet en MasoksTech.API las dependencias requeridas:

Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.InMemory (para el contexto ágil)

[ ] Tarea 1.2: Crear el archivo MasoksTech.API/Models/ModelosBase.cs con las clases de dominio (Categoria, Marca, Accesorio, MovimientoInventario).

[ ] Tarea 1.3: Crear el archivo MasoksTech.API/Data/AppDbContext.cs heredando de DbContext para mapear los DbSet correspondientes.

[ ] Tarea 1.4: Modificar el archivo MasoksTech.API/Program.cs para registrar el AppDbContext en el contenedor de inversión de control (Inyección de Dependencias) usando la base de datos en memoria.

🔀 Fase 2: Capa de Transporte de Datos (API)
[ ] Tarea 2.1: Crear la carpeta DTOs dentro de MasoksTech.API.

[ ] Tarea 2.2: Crear el archivo MasoksTech.API/DTOs/AccesoriosDtos.cs declarando los cuatro records base (CrearAccesorioDto, AccesorioResponseDto, RegistrarVentaDto, RespuestaVentaDto).

⚙️ Fase 3: Implementación de Controladores Lógicos (API)
[ ] Tarea 3.1: Crear MasoksTech.API/Controllers/AccesoriosController.cs incorporando el constructor con la inyección del contexto, el método HttpGet (Listar) y HttpPost (Registrar) con sus validaciones de campos nulos/vacíos.

[ ] Tarea 3.2: Crear MasoksTech.API/Controllers/InventarioController.cs e implementar el endpoint de salida por venta (api/inventario/venta) con el algoritmo de control de stock y bandera de alerta de stock mínimo.

🧪 Fase 4: Enlace y Automatización del Spec Kit (Specs)
[ ] Tarea 4.1: Crear la carpeta Features dentro del proyecto de pruebas MasoksTech.Specs.

[ ] Tarea 4.2: Copiar y guardar los archivos .feature de las especificaciones de comportamiento (CatalogoAccesorios.feature y GestionInventario.feature).

[ ] Tarea 4.3: Vincular el archivo UnitTest1.cs para instanciar clientes HTTP de prueba que invoquen directamente los controladores de la API y comprobar las afirmaciones (Assertions).