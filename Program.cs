using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de datos en memoria
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagerDb"));

builder.Services.AddScoped<ITaskService, TaskService>();

// Permite que la página visual hable con esta API.
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

// Sembramos un par de categorías de ejemplo al arrancar, para probar rápido.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Categories.AddRange(
        new TaskManager.Models.Category { Name = "Trabajo" },
        new TaskManager.Models.Category { Name = "Personal" }
    );
    db.SaveChanges();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
