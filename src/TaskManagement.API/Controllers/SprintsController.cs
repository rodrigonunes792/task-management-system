using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SprintsController : ControllerBase
{
    private readonly ISprintRepository _repository;
    private readonly ILogger<SprintsController> _logger;

    public SprintsController(ISprintRepository repository, ILogger<SprintsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get sprint by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Sprint>> GetById(Guid id)
    {
        try
        {
            var sprint = await _repository.GetByIdAsync(id);
            if (sprint == null)
                return NotFound($"Sprint with ID {id} not found");

            return Ok(sprint);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving sprint {SprintId}", id);
            return StatusCode(500, "An error occurred while retrieving the sprint");
        }
    }

    /// <summary>
    /// Get sprints by project
    /// </summary>
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<Sprint>>> GetByProject(Guid projectId)
    {
        try
        {
            var sprints = await _repository.GetByProjectIdAsync(projectId);
            return Ok(sprints);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving sprints for project {ProjectId}", projectId);
            return StatusCode(500, "An error occurred while retrieving sprints");
        }
    }

    /// <summary>
    /// Create a new sprint
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Sprint>> Create([FromBody] CreateSprintRequest request)
    {
        try
        {
            var sprint = new Sprint(
                request.Name,
                request.Goal,
                request.StartDate,
                request.EndDate,
                request.ProjectId
            );

            var created = await _repository.AddAsync(sprint);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating sprint");
            return StatusCode(500, "An error occurred while creating the sprint");
        }
    }

    /// <summary>
    /// Start sprint
    /// </summary>
    [HttpPost("{id}/start")]
    public async Task<IActionResult> Start(Guid id)
    {
        try
        {
            var sprint = await _repository.GetByIdAsync(id);
            if (sprint == null)
                return NotFound($"Sprint with ID {id} not found");

            sprint.Start();
            await _repository.UpdateAsync(sprint);

            return Ok(new { message = "Sprint started successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting sprint {SprintId}", id);
            return StatusCode(500, "An error occurred while starting the sprint");
        }
    }

    /// <summary>
    /// Complete sprint
    /// </summary>
    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        try
        {
            var sprint = await _repository.GetByIdAsync(id);
            if (sprint == null)
                return NotFound($"Sprint with ID {id} not found");

            sprint.Complete();
            await _repository.UpdateAsync(sprint);

            return Ok(new { message = "Sprint completed successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error completing sprint {SprintId}", id);
            return StatusCode(500, "An error occurred while completing the sprint");
        }
    }

    /// <summary>
    /// Delete sprint
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var sprint = await _repository.GetByIdAsync(id);
            if (sprint == null)
                return NotFound($"Sprint with ID {id} not found");

            await _repository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting sprint {SprintId}", id);
            return StatusCode(500, "An error occurred while deleting the sprint");
        }
    }
}

public record CreateSprintRequest(string Name, string Goal, DateTime StartDate, DateTime EndDate, Guid ProjectId);
