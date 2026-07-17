# 📜 CONSTITUCIÓN DEL PROYECTO: MASOKS TECH

## 1. INFORMACIÓN INSTITUCIONAL Y ACADÉMICA
* **Institución:** Universidad Nacional de San Cristóbal de Huamanga (UNSCH)
* **Facultad:** Ingeniería de Minas, Geología y Civil
* **Escuela Profesional:** Ingeniería de Sistemas
* **Curso:** Pruebas y Aseguramiento de la Calidad de Software
* **Proyecto:** Implementación de un Sistema Web para la Automatización de los Procesos de Compra, Venta y Gestión de Inventario de Accesorios y Suministros para Telefonía Móvil: MASOKS TECH
* **Autor / Estudiante:** Tanta Chala, Maks Brayan
* **Docente / Asesor:** Mg. Ing. Richard Zapata Casaverde
* **Año:** 2026

---

## 2. PRINCIPIOS INAMOVIBLES DE DESARROLLO (QUALITY CONSTITUTION)

Para garantizar el cumplimiento de los estándares exigidos en la cátedra de Calidad de Software, el desarrollo de **MASOKS TECH** se regirá estrictamente bajo las siguientes leyes fundamentales:

### ⚖️ Ley 1: Desarrollo Guiado por Comportamiento (BDD / Spec Kit)
* Ninguna funcionalidad o regla de negocio del núcleo (Inventario, Compras, Ventas) se considerará terminada ("Done") si no posee su respectivo archivo de especificación en lenguaje natural (`.feature`) mediante el Spec Kit (**Reqnroll / SpecFlow**).
* Todas las especificaciones deben redactarse en español estándar utilizando la sintaxis Gherkin (`Dado que... Cuando... Entonces...`).

### 🛠️ Ley 2: Calidad de Código y Arquitectura Limpia
* El proyecto mantendrá una separación estricta de responsabilidades en capas. Los controladores solo orquestarán, los servicios procesarán la lógica de negocio, y el contexto de datos manejará la persistencia.
* Queda estrictamente prohibido acoplar código lógico directamente en los controladores o vistas.

### 🛡️ Ley 3: Cobertura y Automatización de Pruebas
* Todo cambio en el código fuente de `MasoksTech.API` deberá pasar de forma limpia las pruebas de aceptación configuradas en `MasoksTech.Specs`.
* La base de datos de pruebas en memoria o SQLite local debe ser limpiada o reiniciada automáticamente entre escenarios para evitar contaminación de datos.

### ⚡ Ley 4: Rendimiento y Seguridad
* Toda contraseña de usuario almacenada en la base de datos debe encriptarse utilizando algoritmos de derivación de claves o hashing seguros integrados en .NET.
* Las respuestas del API deben estructurarse utilizando DTOs (Data Transfer Objects) para evitar la sobreexposición de entidades internas del sistema.

---

## 3. STACK TECNOLÓGICO Y ENTORNO AUTORIZADO

El ecosistema oficial del proyecto queda fijado de la siguiente manera:

* **IDE de Desarrollo:** Visual Studio Community (Versión 2022 o superior).
* **Framework Backend:** .NET 8.0 (ASP.NET Core Web API).
* **Motor de Base de Datos:** SQLite (`masoks_tech.db`) para portabilidad y consistencia ágil en el entorno local de desarrollo.
* **ORM:** Entity Framework Core (Enfoque Code-First).
* **Motor de Spec Kit (Testing):** Reqnroll integrado con **xUnit** como framework ejecutor de pruebas unitarias y de integración.
* **Documentación Técnica:** OpenAPI / Swagger UI para la exploración interactiva de endpoints.

---

## 4. COMPROMISO DE CALIDAD
Yo, **Maks Brayan Tanta Chala**, como desarrollador y autor de este proyecto de investigación, me comprometo a respetar cada lineamiento establecido en esta constitución, asegurando un producto de software auditable, escalable y metodológicamente sólido para la Escuela Profesional de Ingeniería de Sistemas de la UNSCH.