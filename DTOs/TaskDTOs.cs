using TaskManager.Models;

namespace TaskManager.DTOs
{
    // Esto es lo que se devuelve al cliente en el GET.
    // Nunca exponemos la entidad TaskItem directa, para no filtrar
    // detalles internos ni mandar solo el CategoryId "pelón".
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatusEnum Status { get; set; }
        public string CategoryName { get; set; } = string.Empty; // <- nombre, no el ID
    }

    // Esto es lo que llega en el body cuando se crea una tarea.
    public class CreateTaskDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}
