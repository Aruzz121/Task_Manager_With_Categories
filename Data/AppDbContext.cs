using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación 1:N explícita: una Category -> muchos Tasks
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
