using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _repository;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskRepository repository, ILogger<TasksController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get task by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(Guid id)
    {
        try
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found");

            return Ok(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving task {TaskId}", id);
            return StatusCode(500, "An error occurred while retrieving the task");
        }
    }

    /// <summary>
    /// Get tasks by project
    /// </summary>
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetByProject(Guid projectId)
    {
        try
        {
            var tasks = await _repository.GetByProjectIdAsync(projectId);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving tasks for project {ProjectId}", projectId);
            return StatusCode(500, "An error occurred while retrieving tasks");
        }
    }

    /// <summary>
    /// Get tasks by sprint
    /// </summary>
    [HttpGet("sprint/{sprintId}")]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetBySprint(Guid sprintId)
    {
        try
        {
            var tasks = await _repository.GetBySprintIdAsync(sprintId);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving tasks for sprint {SprintId}", sprintId);
            return StatusCode(500, "An error occurred while retrieving tasks");
        }
    }

    /// <summary>
    /// Create a new task
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create([FromBody] CreateTaskRequest request)
    {
        try
        {
            var task = new TaskItem(
                request.Title,
                request.Description,
                request.Priority,
                request.EstimatedHours,
                request.ProjectId
            );

            var created = await _repository.AddAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating task");
            return StatusCode(500, "An error occurred while creating the task");
        }
    }

    /// <summary>
    /// Update task
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
    {
        try
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found");

            task.UpdateDetails(request.Title, request.Description, request.Priority, request.EstimatedHours);
            await _repository.UpdateAsync(task);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating task {TaskId}", id);
            return StatusCode(500, "An error occurred while updating the task");
        }
    }

    /// <summary>
    /// Update task status
    /// </summary>
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
    {
        try
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found");

            task.UpdateStatus(request.Status);
            await _repository.UpdateAsync(task);

            return Ok(new { message = "Task status updated successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating task status {TaskId}", id);
            return StatusCode(500, "An error occurred while updating task status");
        }
    }

    /// <summary>
    /// Assign task to user
    /// </summary>
    [HttpPatch("{id}/assign")]
    public async Task<IActionResult> Assign(Guid id, [FromBody] AssignTaskRequest request)
    {
        try
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found");

            task.AssignTo(request.UserId);
            await _repository.UpdateAsync(task);

            return Ok(new { message = "Task assigned successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error assigning task {TaskId}", id);
            return StatusCode(500, "An error occurred while assigning the task");
        }
    }

    /// <summary>
    /// Log hours to task
    /// </summary>
    [HttpPost("{id}/log-hours")]
    public async Task<IActionResult> LogHours(Guid id, [FromBody] LogHoursRequest request)
    {
        try
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found");

            task.LogHours(request.Hours);
            await _repository.UpdateAsync(task);

            return Ok(new { message = "Hours logged successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error logging hours for task {TaskId}", id);
            return StatusCode(500, "An error occurred while logging hours");
        }
    }

    /// <summary>
    /// Delete task
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found");

            await _repository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting task {TaskId}", id);
            return StatusCode(500, "An error occurred while deleting the task");
        }
    }
}

public record CreateTaskRequest(string Title, string Description, TaskPriority Priority, int EstimatedHours, Guid ProjectId);
public record UpdateTaskRequest(string Title, string Description, TaskPriority Priority, int EstimatedHours);
public record UpdateStatusRequest(TaskManagement.Domain.Entities.TaskStatus Status);
public record AssignTaskRequest(Guid UserId);
public record LogHoursRequest(int Hours);
