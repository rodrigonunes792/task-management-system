using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(IProjectRepository repository, ILogger<ProjectsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get all projects
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetAll()
    {
        try
        {
            var projects = await _repository.GetAllAsync();
            return Ok(projects);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving projects");
            return StatusCode(500, "An error occurred while retrieving projects");
        }
    }

    /// <summary>
    /// Get project by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetById(Guid id)
    {
        try
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found");

            return Ok(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving project {ProjectId}", id);
            return StatusCode(500, "An error occurred while retrieving the project");
        }
    }

    /// <summary>
    /// Create a new project
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Project>> Create([FromBody] CreateProjectRequest request)
    {
        try
        {
            var project = new Project(
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate
            );

            var created = await _repository.AddAsync(project);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating project");
            return StatusCode(500, "An error occurred while creating the project");
        }
    }

    /// <summary>
    /// Update project
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectRequest request)
    {
        try
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found");

            project.UpdateDetails(request.Name, request.Description);
            await _repository.UpdateAsync(project);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating project {ProjectId}", id);
            return StatusCode(500, "An error occurred while updating the project");
        }
    }

    /// <summary>
    /// Complete project
    /// </summary>
    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        try
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found");

            project.Complete();
            await _repository.UpdateAsync(project);

            return Ok(new { message = "Project completed successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error completing project {ProjectId}", id);
            return StatusCode(500, "An error occurred while completing the project");
        }
    }

    /// <summary>
    /// Delete project
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found");

            await _repository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting project {ProjectId}", id);
            return StatusCode(500, "An error occurred while deleting the project");
        }
    }
}

public record CreateProjectRequest(string Name, string Description, DateTime StartDate, DateTime? EndDate);
public record UpdateProjectRequest(string Name, string Description);
