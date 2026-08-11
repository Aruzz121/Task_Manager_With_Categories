# Challenge 3 — Task Manager with Categories

API en **ASP.NET Core + Entity Framework Core**, con una interfaz visual propia (sin necesidad de Postman ni Swagger para probarla) para organizar tareas por categoría.

## Qué incluye

### Backend (API)

- **Modelos** (`Models/`): `Category` y `TaskItem` (`Id, Title, Description, Status, CategoryId`), con relación 1:N (una categoría tiene muchas tareas) vía `CategoryId`. `Description` es opcional.
- **DTOs** (`DTOs/`): `TaskDTO` (nunca se expone la entidad cruda; incluye el nombre de la categoría en vez de solo el ID), `CreateTaskDTO`, `CategoryDTO` y `CreateCategoryDTO`.
- **Servicio** (`Services/TaskService.cs`): antes de guardar o editar una tarea, valida que el `CategoryId` exista en la base de datos; si no existe, regresa un error claro. También arma los filtros del GET.
- **Controladores**:
  - `TasksController`: `GET /api/tasks` (con filtros opcionales e independientes por `categoryId` y `status`), `POST /api/tasks`, `PUT /api/tasks/{id}` (editar título/categoría), `PATCH /api/tasks/{id}/toggle-status` (marcar pendiente/completada) y `DELETE /api/tasks/{id}`.
  - `CategoriesController`: `GET /api/categories` y `POST /api/categories` (crear categorías nuevas, valida que no exista una con el mismo nombre).
- **Program.cs**: usa **SQLite** (`taskmanager.db`, un archivo que se crea solo en la carpeta del proyecto) para que los datos se queden guardados de verdad, aunque cierres la terminal o reinicies tu compu. Siembra dos categorías de ejemplo ("Trabajo" y "Personal") solo la primera vez que corres el proyecto. Tiene CORS abierto y sirve archivos estáticos desde `wwwroot`.

### Interfaz visual (`wwwroot/index.html`)

Página que consume la API directamente, sin frameworks — es HTML, CSS y JavaScript puro. Incluye:

- Formulario para **agregar tareas**, con título, descripción opcional y categoría.
- Botón **"+ Categoría"** para crear categorías nuevas al vuelo, sin salir de la página.
- Filtros por **categoría** y por **estado** (pendiente/completado), funcionando de forma independiente.
- En cada tarea: casilla para **marcarla como completada**, ícono de **lápiz para editarla** (cambiar el texto y/o la categoría) e ícono de **bote de basura para eliminarla**.
- Colores automáticos por categoría, para identificarlas de un vistazo.

## Cómo correrlo

Desde la carpeta del proyecto (donde está `TaskManager.csproj`):

```bash
dotnet restore
dotnet run
```

Cuando veas en la terminal la línea `Now listening on: http://localhost:5000`, abre en tu navegador:

```
http://localhost:5000/
```

Ahí se abre directamente la interfaz visual. Deja la terminal abierta mientras la uses — si la cierras, la API se apaga y la página deja de funcionar.

## Probar la API directamente (opcional)

Si quieres ver los endpoints "en crudo" (útil para revisar la estructura de las peticiones), puedes activar Swagger:

```bash
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

Y entrar a `http://localhost:5000/swagger`.

## Ejemplos con curl

Crear una categoría:
```bash
curl -X POST http://localhost:5000/api/categories \
  -H "Content-Type: application/json" \
  -d '{"name": "Escuela"}'
```

Crear una tarea:
```bash
curl -X POST http://localhost:5000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{"title": "Terminar el reporte", "description": "Incluir gráficas del Q2", "categoryId": 1}'
```

Si mandas un `categoryId` que no existe, la API regresa un 400 con el mensaje de error en vez de romperse.

Filtrar tareas:
```bash
curl http://localhost:5000/api/tasks?categoryId=1
curl http://localhost:5000/api/tasks?status=1
curl "http://localhost:5000/api/tasks?categoryId=1&status=0"
```
(`status`: 0 = Pendiente, 1 = Completado)

## Nota sobre la base de datos

Usa **SQLite**: al correr `dotnet run` por primera vez, se crea automáticamente un archivo `taskmanager.db` dentro de la carpeta del proyecto. Ahí se guardan tus tareas y categorías de verdad — no se borran al cerrar la terminal ni al reiniciar tu compu. Si en algún momento quieres empezar de cero, basta con borrar ese archivo `taskmanager.db` y volver a correr `dotnet run`; se vuelve a crear vacío (con "Trabajo" y "Personal" sembradas de nuevo).
