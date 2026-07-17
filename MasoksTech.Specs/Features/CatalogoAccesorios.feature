using System;
using System.Collections.Generic;
using System.Text;

namespace MasoksTech.Specs.Features
{
    class CatalogoAccesorios
    {
    }
}
# language: es
Característica: Gestión del Catálogo de Accesorios y Suministros
    Como Administrador del sistema MASOKS TECH
    Quiero registrar y clasificar los accesorios de telefonía móvil
    Para mantener un catálogo virtual actualizado y visible para los clientes.

    Antecedentes:
        Dado que el sistema cuenta con las categorías "Cargadores" y "Protección"
        Y cuenta con las marcas comerciales "Apple" y "Samsung"

    @AltaPrioridad @Catalogo
    Escenario: Registro exitoso de un nuevo accesorio en el catálogo
        Dado que el usuario se ha autenticado con el rol "Administrador"
        Cuando solicita registrar un accesorio con los siguientes datos:
            | Nombre                            | Marca   | Categoría  | Precio | Stock Inicial |
            | Cargador Carga Rápida 20W Tipo C  | Apple   | Cargadores | 45.00  | 50            |
        Entonces el sistema debe guardar el accesorio en el catálogo comercial
        Y la respuesta de la API debe confirmar el código de estado HTTP 201 (Creado)

    @Validacion @Catalogo
    Escenario: Rechazar el registro de un accesorio sin marca o categoría asignada
        Dado que el usuario se ha autenticado con el rol "Administrador"
        Cuando intenta registrar un accesorio con los siguientes datos:
            | Nombre                       | Marca | Categoría | Precio | Stock Inicial |
            | Funda Silicona Transparente  |       |           | 25.00  | 10            |
        Entonces el sistema debe denegar el registro del producto
        Y debe mostrar un mensaje de error indicando "No podrá registrarse un producto sin categoría ni marca"