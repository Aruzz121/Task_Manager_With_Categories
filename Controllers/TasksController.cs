using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET /api/tasks
        // GET /api/tasks?categoryId=2
        // GET /api/tasks?status=Completed
        // GET /api/tasks?categoryId=2&status=Pending
        [HttpGet]
        public async Task<ActionResult<List<TaskDTO>>> GetTasks(
            [FromQuery] int? categoryId,
            [FromQuery] TaskStatusEnum? status)
        {
            var tasks = await _taskService.GetTasksAsync(categoryId, status);
            return Ok(tasks);
        }

        // POST /api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateTask([FromBody] CreateTaskDTO dto)
        {
            var result = await _taskService.CreateTaskAsync(dto);

            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return CreatedAtAction(nameof(GetTasks), new { id = result.Data!.Id }, result.Data);
        }

        // PATCH /api/tasks/{id}/toggle-status
        [HttpPatch("{id}/toggle-status")]
        public async Task<ActionResult<TaskDTO>> ToggleStatus(int id)
        {
            var result = await _taskService.ToggleStatusAsync(id);

            if (!result.Success)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // PUT /api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDTO>> UpdateTask(int id, [FromBody] CreateTaskDTO dto)
        {
            var result = await _taskService.UpdateTaskAsync(id, dto);

            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // DELETE /api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            if (!deleted)
                return NotFound(new { message = $"No existe una tarea con Id {id}." });

            return NoContent();
        }
    }
}
