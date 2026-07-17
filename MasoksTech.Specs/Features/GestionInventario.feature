using System;
using System.Collections.Generic;
using System.Text;

namespace MasoksTech.Specs.Features
{
    class GestionInventario
    {
    }
}
# language: es
Característica: Control de Inventario y Movimientos de Almacén
    Como Encargado de Almacén / Sistema Automático
    Quiero controlar las existencias de suministros y procesar variaciones de stock
    Para prevenir inconsistencias y evitar la venta de productos agotados.

    @AltaPrioridad @Inventario
    Escenario: Descuento automático de existencias tras una venta confirmada
        Dado que el accesorio "Funda Antigolpes Samsung S23" tiene un stock disponible de 15 unidades
        Cuando un cliente finaliza una compra en línea por 2 unidades de dicho accesorio
        Entonces el sistema debe procesar la disminución automática del stock
        Y el inventario actual de "Funda Antigolpes Samsung S23" debe reflejar un saldo exacto de 13 unidades

    @Restriccion @Inventario
    Escenario: Impedir la venta de un accesorio que no cuenta con stock disponible
        Dado que el accesorio "Mica de Vidrio Templado iPhone 14" tiene un stock disponible de 0 unidades
        Cuando un cliente intenta agregar e iniciar la compra de 1 unidad de dicho accesorio
        Entonces el sistema debe bloquear la transacción comercial
        Y debe retornar un mensaje advirtiendo "No se permitirá vender productos sin stock disponible"

    @Alertas @Inventario
    Escenario: Activación de alerta por nivel crítico de stock mínimo
        Dado que el accesorio "Audífonos Bluetooth Pro" tiene un stock actual de 6 unidades
        Cuando se registra una salida de almacén de 4 unidades por venta presencial
        Entonces el stock actual debe actualizarse a 2 unidades
        Y el sistema debe lanzar una alerta de stock mínimo indicando que el producto requiere reposición