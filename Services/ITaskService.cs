using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };
        public static ServiceResult<T> Fail(string error) => new() { Success = false, ErrorMessage = error };
    }

    public interface ITaskService
    {
        Task<List<TaskDTO>> GetTasksAsync(int? categoryId, TaskStatusEnum? status);
        Task<ServiceResult<TaskDTO>> CreateTaskAsync(CreateTaskDTO dto);
        Task<ServiceResult<TaskDTO>> ToggleStatusAsync(int id);
        Task<ServiceResult<TaskDTO>> UpdateTaskAsync(int id, CreateTaskDTO dto);
        Task<bool> DeleteTaskAsync(int id);
    }
}
