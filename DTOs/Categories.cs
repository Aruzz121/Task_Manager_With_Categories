namespace TaskManager.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CreateCategoryDTO
    {
        public string Name { get; set; } = string.Empty;
    }
}
