using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id);
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project> AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Guid id);
}

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);
    Task<IEnumerable<TaskItem>> GetBySprintIdAsync(Guid sprintId);
    Task<TaskItem> AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);
}

public interface ISprintRepository
{
    Task<Sprint?> GetByIdAsync(Guid id);
    Task<IEnumerable<Sprint>> GetByProjectIdAsync(Guid projectId);
    Task<Sprint> AddAsync(Sprint sprint);
    Task UpdateAsync(Sprint sprint);
    Task DeleteAsync(Guid id);
}
