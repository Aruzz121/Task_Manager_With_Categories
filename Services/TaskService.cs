using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        // GET con filtros opcionales e independientes por CategoryId y/o Status
        public async Task<List<TaskDTO>> GetTasksAsync(int? categoryId, TaskStatusEnum? status)
        {
            var query = _context.Tasks
                .Include(t => t.Category)
                .AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(t => t.CategoryId == categoryId.Value);

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            return await query
                .Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    CategoryName = t.Category!.Name
                })
                .ToListAsync();
        }

        // Antes de guardar, se valida que el CategoryId exista de verdad
        public async Task<ServiceResult<TaskDTO>> CreateTaskAsync(CreateTaskDTO dto)
        {
            var category = await _context.Categories.FindAsync(dto.CategoryId);

            if (category == null)
            {
                return ServiceResult<TaskDTO>.Fail(
                    $"No existe una categoría con Id {dto.CategoryId}.");
            }

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Status = TaskStatusEnum.Pending
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var resultDto = new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CategoryName = category.Name
            };

            return ServiceResult<TaskDTO>.Ok(resultDto);
        }

        // Cambia Pending <-> Completed
        public async Task<ServiceResult<TaskDTO>> ToggleStatusAsync(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                return ServiceResult<TaskDTO>.Fail($"No existe una tarea con Id {id}.");

            task.Status = task.Status == TaskStatusEnum.Pending
                ? TaskStatusEnum.Completed
                : TaskStatusEnum.Pending;

            await _context.SaveChangesAsync();

            return ServiceResult<TaskDTO>.Ok(new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CategoryName = task.Category!.Name
            });
        }

        // Edita título y/o categoría de una tarea existente, con la misma validación de CategoryId
        public async Task<ServiceResult<TaskDTO>> UpdateTaskAsync(int id, CreateTaskDTO dto)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
                return ServiceResult<TaskDTO>.Fail($"No existe una tarea con Id {id}.");

            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null)
                return ServiceResult<TaskDTO>.Fail($"No existe una categoría con Id {dto.CategoryId}.");

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.CategoryId = dto.CategoryId;
            await _context.SaveChangesAsync();

            return ServiceResult<TaskDTO>.Ok(new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CategoryName = category.Name
            });
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
