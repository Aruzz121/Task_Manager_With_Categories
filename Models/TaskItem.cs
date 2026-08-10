namespace TaskManager.Models
{
    public enum TaskStatusEnum
    {
        Pending,
        Completed
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;

        // Llave foránea (FK) hacia Category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
