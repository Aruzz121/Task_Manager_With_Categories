namespace TaskManager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Relación 1:N -> una categoría tiene muchas tareas
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
