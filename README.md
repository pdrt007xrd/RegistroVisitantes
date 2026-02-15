# 📋 Registro de Visitantes

Sistema de gestión de visitantes desarrollado con **ASP.NET Core 10** y **Bootstrap 5**.

## 🎯 Características

- ✅ Registro de visitantes con formulario responsive
- ✅ Búsqueda y filtrado por nombre, empresa y rangos de fecha
- ✅ Paginación de resultados (5 registros por página)
- ✅ Formato automático de cédula (XXX-XXXXXXX-X)
- ✅ Generación de reportes PDF
- ✅ Interfaz 100% responsive (móvil, tablet, desktop)
- ✅ Base de datos SQL Server con Entity Framework Core
- ✅ Notificaciones visuales de éxito

## 📋 Requisitos Previos

- **.NET 10 SDK** o superior
- **SQL Server** (MSSQL) instalado y corriendo
- **Git** (opcional, para clonar el repositorio)

## 🚀 Instalación Rápida

### Opción 1: Script Automático (Recomendado)

```bash
chmod +x setup.sh
sudo ./setup.sh
```

El script automáticamente:
- Verifica las dependencias (.NET SDK, SQL Server)
- Restaura los paquetes NuGet
- Crea/Actualiza la base de datos
- Inicia la aplicación en http://localhost:5000

### Opción 2: Instalación Manual

#### 1. Clonar el repositorio
```bash
git clone https://github.com/pdrt007xrd/RegistroVisitantes.git
cd RegistroVisitantes
```

#### 2. Restaurar dependencias de NuGet
```bash
dotnet restore
```

#### 3. Configurar la conexión a la base de datos

Editar `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=VisitDb;User Id=sa;Password=TU_PASSWORD;Encrypt=True;TrustServerCertificate=True;"
  }
}
```

Reemplaza `TU_PASSWORD` con tu contraseña de SQL Server.

#### 4. Crear/Actualizar la base de datos
```bash
dotnet ef database update
```

#### 5. Ejecutar la aplicación
```bash
dotnet run --urls=http://localhost:5000
```

## 🌐 Acceso a la Aplicación

Una vez iniciada, accede a:
```
http://localhost:5000
```

## 📦 Estructura del Proyecto

```
VisitasApi/
├── Controllers/
│   ├── HomeController.cs       # Controlador MVC
│   └── VisitasController.cs    # API REST para visitantes
├── Views/
│   └── Home/
│       └── Index.cshtml        # Vista principal con Bootstrap
├── Models/
│   └── Models.cs               # Entidades (Contacto, VisitasContext)
├── Migrations/                 # Migraciones Entity Framework
├── Program.cs                  # Configuración de la aplicación
├── appsettings.json            # Configuración
└── VisitasApi.csproj          # Archivo de proyecto
```

## 🔧 Tecnologías Utilizadas

- **ASP.NET Core 10** - Framework web
- **Entity Framework Core 10** - ORM
- **SQL Server** - Base de datos
- **Bootstrap 5** - Framework CSS
- **QuestPDF** - Generación de reportes PDF

## 📝 Funcionalidades Principales

### Agregar Contacto
- Nombre (obligatorio)
- Cédula en formato XXX-XXXXXXX-X (obligatorio)
- Empresa Proveniente
- Dónde Visita
- Fecha automática

### Buscar y Filtrar
- Búsqueda por nombre o empresa
- Filtrado por rango de fechas
- Paginación automática

### Generar Reportes
- Exportar a PDF con datos filtrados
- Visualización en modal
- Incluye total de registros

## 🛠️ Comandos Útiles

```bash
# Compilar el proyecto
dotnet build

# Ejecutar en modo debug
dotnet run

# Ejecutar en modo watch (recompila automáticamente)
dotnet watch run

# Crear una nueva migración
dotnet ef migrations add NombreMigracion

# Aplicar migraciones a la BD
dotnet ef database update

# Limpiar archivos compilados
dotnet clean
```

## ⚠️ Solución de Problemas

### Error: "Address already in use"
```bash
# Liberar el puerto 5000
sudo lsof -i :5000 | grep LISTEN | awk '{print $2}' | xargs kill -9
```

### Error: "Failed to connect to database"
- Verificar que SQL Server esté corriendo
- Verificar las credenciales en `appsettings.json`
- Verificar la cadena de conexión

### Error: ".NET SDK not found"
- Instalar .NET 10 SDK desde: https://dotnet.microsoft.com/download

## 📄 Licencia

Este proyecto es de código abierto.

## 👨‍💻 Autor

Desarrollado por: pdrt007xrd

## 📞 Soporte

Para reportar problemas o sugerencias, abre un issue en GitHub.

---

**Última actualización**: 15 de febrero de 2026
