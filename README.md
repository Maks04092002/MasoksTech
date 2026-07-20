# 📱 MASOKS TECH

## Sistema Web Escalable para la Automatización de Procesos de Compra, Venta y Gestión de Inventario de Accesorios y Suministros para Telefonía Móvil

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-.NET%2010-purple)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-blue)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-Core-green)
![License](https://img.shields.io/badge/License-Academic-orange)

---

# 📖 Descripción

MASOKS TECH es una plataforma web desarrollada para optimizar los procesos comerciales de una empresa dedicada a la venta de accesorios y suministros para telefonía móvil.

El sistema automatiza las operaciones de compra, venta y gestión de inventario, proporcionando una interfaz intuitiva para administradores y usuarios, mejorando la eficiencia operativa y facilitando la administración de productos.

---

# 🎯 Objetivos

- Automatizar el proceso de ventas.
- Gestionar el inventario en tiempo real.
- Administrar usuarios y permisos.
- Optimizar el registro de productos.
- Facilitar la administración del negocio mediante una plataforma web.

---

# 🚀 Tecnologías Utilizadas

- ASP.NET Core MVC (.NET 10)
- C#
- Entity Framework Core
- PostgreSQL
- HTML5
- CSS3
- Bootstrap 5
- JavaScript
- Git
- GitHub
- Visual Studio Community 2026

---

# 📂 Estructura del Proyecto

```
MasoksTech
│
├── MasoksTech.Web
│   ├── Controllers
│   ├── Models
│   ├── Views
│   ├── Data
│   ├── Migrations
│   └── wwwroot
│
├── MasoksTech.API
│
├── MasoksTech.Specs
│
├── MasoksTech.Web.Tests
│
└── MasoksTech.slnx
```

---

# ⚙️ Requisitos

- Visual Studio Community 2026
- .NET SDK 10
- PostgreSQL 16 o superior
- Git

---

# 🔧 Instalación

## 1. Clonar el repositorio

```bash
git clone https://github.com/TU-USUARIO/MasoksTech.git
```

## 2. Entrar al proyecto

```bash
cd MasoksTech
```

## 3. Restaurar dependencias

```bash
dotnet restore
```

## 4. Configurar la cadena de conexión

Editar el archivo:

```
appsettings.json
```

Ejemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=MasoksTech;Username=postgres;Password=tu_password"
}
```

---

## 5. Ejecutar migraciones

```bash
dotnet ef database update
```

---

## 6. Ejecutar el proyecto

```bash
dotnet run --project MasoksTech.Web
```

---

# 🗄️ Base de Datos

El proyecto utiliza PostgreSQL junto con Entity Framework Core para la gestión de la persistencia de datos.

Las migraciones se encuentran en:

```
MasoksTech.Web/Migrations
```

---

# 📌 Funcionalidades

- Inicio de sesión
- Gestión de usuarios
- Gestión de productos
- Control de inventario
- Registro de ventas
- Administración de categorías
- Panel administrativo
- Interfaz responsiva

---

# 🏗️ Arquitectura

El sistema está organizado en proyectos independientes siguiendo una arquitectura por capas:

- Presentación (ASP.NET Core MVC)
- Lógica de Negocio
- Acceso a Datos
- API REST
- Pruebas

---

# 🧪 Pruebas

El proyecto incluye pruebas dentro de:

```
MasoksTech.Web.Tests
```

---

# 📸 Capturas

Puedes agregar aquí imágenes del sistema.

Ejemplo:

```
docs/login.png
docs/dashboard.png
docs/productos.png
```

---

# 📈 Estado del Proyecto

🟢 En desarrollo

Actualmente el proyecto continúa incorporando mejoras y nuevas funcionalidades.

---

# 👨‍💻 Autor

**Maks Brayan Tanta Chala**

Universidad Nacional de San Cristóbal de Huamanga

Escuela Profesional de Ingeniería de Sistemas

Ayacucho - Perú

2026

---

# 📄 Licencia

Este proyecto fue desarrollado con fines académicos como parte del Proyecto Final de Ingeniería de Sistemas.

Su uso es exclusivamente educativo.
