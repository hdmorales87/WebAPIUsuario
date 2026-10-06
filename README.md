# WebAPIUsuario

API REST para gestión de usuarios desarrollada con .NET 10, siguiendo arquitectura limpia y mejores prácticas de desarrollo.

## 📋 Índice

- [Stack Tecnológico](#stack-tecnológico)
- [Arquitectura del Proyecto](#arquitectura-del-proyecto)
- [Estructura de Carpetas](#estructura-de-carpetas)
- [Configuración de Variables de Entorno](#configuración-de-variables-de-entorno)
- [API Endpoints](#api-endpoints)
- [DTOs](#dtos)
- [Ejecución del Proyecto](#ejecución-del-proyecto)

## 🛠 Stack Tecnológico

- **.NET 10.0**: Framework principal
- **Entity Framework Core 10.0**: ORM para acceso a datos
- **SQL Server 2022**: Base de datos
- **ASP.NET Core**: Framework web
- **Swagger/OpenAPI**: Documentación de API
- **DotNetEnv**: Gestión de variables de entorno
- **Docker**: Contenedorización

## 🏗 Arquitectura del Proyecto

El proyecto sigue una arquitectura en capas con desacoplamiento de responsabilidades, implementando el patrón Repository para separar la lógica de negocio del acceso a datos:

```
┌─────────────────────────────────────┐
│   Controllers (Capa Presentación)   │
│   - Manejo de solicitudes HTTP      │
│   - Validación de DTOs              │
│   - Códigos de estado HTTP          │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Services (Capa Lógica de Negocio) │
│   - Lógica de negocio               │
│   - Mapeo de entidades a DTOs       │
│   - Orquestación de operaciones     │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Repositories (Capa de Repositorio)│
│   - Abstracción del acceso a datos  │
│   - Operaciones CRUD genéricas      │
│   - Consultas especializadas        │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   Data Access (Capa Persistencia)    │
│   - DbContext                       │
│   - Entity Framework                │
│   - Entidades de dominio            │
└─────────────────────────────────────┘
```

### Patrones Utilizados

- **Dependency Injection**: Inyección de dependencias para servicios, repositorios y DbContext
- **Repository Pattern**: Abstracción del acceso a datos mediante interfaces de repositorio
- **DTO Pattern**: Desacoplamiento de requests/responses de entidades de persistencia
- **Service Layer**: Lógica de negocio separada de la capa de presentación
- **Unit of Work**: Gestión transaccional implícita vía DbContext de EF Core

### Beneficios de la Arquitectura

- **Desacoplamiento**: La capa de servicio no depende directamente de Entity Framework
- **Testabilidad**: Facilita el unit testing mediante mocks de repositorios
- **Mantenibilidad**: Cambios en la tecnología de persistencia no afectan la lógica de negocio
- **Reutilización**: Los repositorios pueden ser reutilizados por múltiples servicios

## 📁 Estructura de Carpetas

```
WebAPIUsuario/
├── Controllers/          # Controladores API
│   └── UsuarioController.cs
├── Services/             # Lógica de negocio
│   ├── IUsuarioService.cs      # Interfaz de servicio
│   └── UsuarioService.cs       # Implementación
├── Repositories/         # Capa de repositorio (acceso a datos)
│   ├── IUsuarioRepository.cs   # Interfaz de repositorio
│   └── UsuarioRepository.cs    # Implementación
├── DTOs/                  # Data Transfer Objects
│   ├── CreateUsuarioDto.cs    # DTO para creación
│   ├── UpdateUsuarioDto.cs    # DTO para actualización
│   └── UsuarioResponseDto.cs  # DTO para respuestas
├── Data/                  # Capa de persistencia
│   └── AppDbContext.cs         # DbContext de EF Core
├── Models/                # Entidades de dominio
│   └── Usuario.cs
├── Migrations/            # Migraciones de EF Core
├── Properties/            # Configuración del proyecto
│   └── launchSettings.json
├── Program.cs             # Punto de entrada y configuración DI
├── appsettings.json       # Configuración de la aplicación
└── WebAPIUsuario.csproj   # Archivo de proyecto
```

## 🔧 Configuración de Variables de Entorno

Este proyecto utiliza variables de entorno para manejar las credenciales de la base de datos, siguiendo las mejores prácticas de seguridad.

### Archivos de Configuración

- **`.env.example`**: Plantilla con las variables de entorno necesarias (sin valores sensibles)
- **`.env`**: Archivo local para desarrollo (NO incluido en git)
- **`.env.docker`**: Archivo específico para Docker Compose (NO incluido en git)

### Configuración Local

1. Copia la plantilla de variables de entorno:
   ```bash
   cp .env.example .env
   ```

2. Edita `.env` con tus credenciales de base de datos

3. Ejecuta la aplicación:
   ```bash
   cd WebAPIUsuario
   dotnet run
   ```

### Configuración con Docker

1. Copia la plantilla:
   ```bash
   cp .env.example .env.docker
   ```

2. Edita `.env.docker` con tus credenciales para Docker

3. Levanta los contenedores:
   ```bash
   docker compose --env-file .env.docker up -d
   ```

### Variables de Entorno

| Variable | Descripción | Default |
|----------|-------------|---------|
| DB_SERVER | Servidor de base de datos | localhost |
| DB_PORT | Puerto de base de datos | 1433 |
| DB_NAME | Nombre de la base de datos | db_usuario |
| DB_USER | Usuario de base de datos | sa |
| DB_PASSWORD | Contraseña de base de datos | **(Requerido)** |
| TRUST_SERVER_CERTIFICATE | Certificado de servidor | True |
| DOCKER_SA_PASSWORD | Contraseña SA para Docker | **(Requerido)** |

⚠️ **IMPORTANTE**: Nunca commits los archivos `.env` o `.env.docker` al repositorio. Estos archivos están excluidos por `.gitignore`.

## 🌐 API Endpoints

### Usuarios

| Método | Endpoint | Descripción | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| GET | `/api/Usuario` | Obtener todos los usuarios | - | `UsuarioResponseDto[]` |
| GET | `/api/Usuario/{id}` | Obtener usuario por ID | - | `UsuarioResponseDto` |
| POST | `/api/Usuario` | Crear nuevo usuario | `CreateUsuarioDto` | `UsuarioResponseDto` (201) |
| PUT | `/api/Usuario/{id}` | Actualizar usuario | `UpdateUsuarioDto` | `UsuarioResponseDto` |
| DELETE | `/api/Usuario/{id}` | Eliminar usuario | - | 204 No Content |

### Ejemplos de Uso

#### Obtener todos los usuarios
```bash
curl -X GET http://localhost:5239/api/Usuario
```

#### Obtener usuario por ID
```bash
curl -X GET http://localhost:5239/api/Usuario/1
```

#### Crear usuario
```bash
curl -X POST http://localhost:5239/api/Usuario \
  -H "Content-Type: application/json" \
  -d '{
    "nombres": "Juan",
    "apellidos": "Pérez",
    "correo": "juan.perez@example.com",
    "username": "juanperez"
  }'
```

#### Actualizar usuario
```bash
curl -X PUT http://localhost:5239/api/Usuario/1 \
  -H "Content-Type: application/json" \
  -d '{
    "nombres": "Juan Carlos",
    "apellidos": "Pérez García",
    "correo": "juan.carlos@example.com",
    "username": "juancarlos"
  }'
```

#### Eliminar usuario
```bash
curl -X DELETE http://localhost:5239/api/Usuario/1
```

## � Capa de Repositorio

### IUsuarioRepository
Interfaz que define las operaciones de acceso a datos para la entidad Usuario. Abstrae la lógica de persistencia y permite la inyección de dependencias.

```csharp
public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario?> UpdateAsync(Usuario usuario);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
```

### UsuarioRepository
Implementación concreta que utiliza Entity Framework Core para interactuar con la base de datos.

**Características:**
- Ciclo de vida `Scoped` en inyección de dependencias
- Operaciones asíncronas para mejor rendimiento
- Manejo de DbContext para persistencia
- Validación de existencia de entidades antes de operaciones

**Ventajas de esta implementación:**
- La capa de servicio no depende directamente de EF Core
- Facilita pruebas unitarias mediante mocks
- Permite cambiar la tecnología de persistencia sin afectar la lógica de negocio
- Centraliza la lógica de acceso a datos

## �📦 DTOs

### CreateUsuarioDto
DTO para la creación de usuarios. Incluye validaciones de datos.

```csharp
public class CreateUsuarioDto
{
    [Required]
    [MaxLength(50)]
    public string Nombres { get; set; }

    [Required]
    [MaxLength(50)]
    public string Apellidos { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Correo { get; set; }

    [MaxLength(100)]
    public string? Username { get; set; }
}
```

### UpdateUsuarioDto
DTO para la actualización de usuarios. Similar a CreateUsuarioDto pero sin incluir campos auto-generados.

```csharp
public class UpdateUsuarioDto
{
    [Required]
    [MaxLength(50)]
    public string Nombres { get; set; }

    [Required]
    [MaxLength(50)]
    public string Apellidos { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Correo { get; set; }

    [MaxLength(100)]
    public string? Username { get; set; }
}
```

### UsuarioResponseDto
DTO para las respuestas de la API. Contiene todos los campos visibles para el cliente.

```csharp
public class UsuarioResponseDto
{
    public int Id { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public string? Username { get; set; }
    public DateTime? FechaCreacion { get; set; }
}
```

## 🚀 Ejecución del Proyecto

### Prerrequisitos

- .NET 10.0 SDK
- SQL Server 2022 (o Docker para contenedorización)
- Docker (opcional, para ejecución en contenedores)

### Pasos de Ejecución

1. **Clonar el repositorio**
   ```bash
   git clone <repository-url>
   cd WebAPIUsuario
   ```

2. **Configurar variables de entorno**
   ```bash
   cp .env.example .env
   # Editar .env con las credenciales correspondientes
   ```

3. **Levantar base de datos con Docker**
   ```bash
   docker compose --env-file .env.docker up -d
   ```

4. **Ejecutar la aplicación**
   ```bash
   cd WebAPIUsuario
   dotnet run
   ```

5. **Acceder a Swagger UI**
   ```
   http://localhost:5239/swagger
   ```

### Comandos Útiles

```bash
# Compilar el proyecto
dotnet build

# Ejecutar en modo desarrollo
dotnet run

# Restaurar dependencias
dotnet restore

# Agregar una nueva dependencia (paquete NuGet)
dotnet add package <nombre-paquete>

# Verificar el código
dotnet format --verify-no-changes
```

## 📝 Notas Adicionales

- La aplicación configura automáticamente la fecha de creación al crear un usuario
- La documentación de la API está disponible vía Swagger en `/swagger`
- Las migraciones de Entity Framework se gestionan con `dotnet ef migrations`
- El proyecto utiliza inyección de dependencias con ciclo de vida `Scoped` para los servicios
