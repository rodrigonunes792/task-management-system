using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Tasks)
            .Include(p => p.Sprints)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects
            .Include(p => p.Tasks)
            .Include(p => p.Sprints)
            .ToListAsync();
    }

    public async Task<Project> AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.Sprint)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.Tasks
            .Where(t => t.ProjectId == projectId)
            .Include(t => t.Sprint)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetBySprintIdAsync(Guid sprintId)
    {
        return await _context.Tasks
            .Where(t => t.SprintId == sprintId)
            .ToListAsync();
    }

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}

public class SprintRepository : ISprintRepository
{
    private readonly ApplicationDbContext _context;

    public SprintRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Sprint?> GetByIdAsync(Guid id)
    {
        return await _context.Sprints
            .Include(s => s.Tasks)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Sprint>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.Sprints
            .Where(s => s.ProjectId == projectId)
            .Include(s => s.Tasks)
            .ToListAsync();
    }

    public async Task<Sprint> AddAsync(Sprint sprint)
    {
        _context.Sprints.Add(sprint);
        await _context.SaveChangesAsync();
        return sprint;
    }

    public async Task UpdateAsync(Sprint sprint)
    {
        _context.Sprints.Update(sprint);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var sprint = await _context.Sprints.FindAsync(id);
        if (sprint != null)
        {
            _context.Sprints.Remove(sprint);
            await _context.SaveChangesAsync();
        }
    }
}
