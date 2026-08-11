using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de datos SQLite: guarda todo en un archivo real (taskmanager.db) en esta carpeta,
// así los datos NO se borran cada vez que detienes y vuelves a correr el proyecto.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=taskmanager.db"));

builder.Services.AddScoped<ITaskService, TaskService>();

// Permite que la página visual (un archivo .html abierto aparte) hable con esta API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Se crea el archivo taskmanager.db si no existe, y solo sembramos
// las categorías de ejemplo la primera vez (si la tabla está vacía).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new TaskManager.Models.Category { Name = "Trabajo" },
            new TaskManager.Models.Category { Name = "Personal" }
        );
        db.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
