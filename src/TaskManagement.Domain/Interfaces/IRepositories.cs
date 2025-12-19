using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces;

/// <summary>
/// Read operations interface for Project repository
/// Follows Interface Segregation Principle
/// </summary>
public interface IProjectReadRepository
{
        Task<Project?> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllAsync();
}

/// <summary>
/// Write operations interface for Project repository
/// Follows Interface Segregation Principle
/// </summary>
public interface IProjectWriteRepository
{
        Task<Project> AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Guid id);
}

/// <summary>
/// Complete repository interface for Project
/// Aggregates read and write operations
/// </summary>
public interface IProjectRepository : IProjectReadRepository, IProjectWriteRepository;

/// <summary>
/// Read operations interface for Task repository
/// Follows Interface Segregation Principle
/// </summary>
public interface ITaskReadRepository
{
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskItem>> GetBySprintIdAsync(Guid sprintId);
}

/// <summary>
/// Write operations interface for Task repository
/// Follows Interface Segregation Principle
/// </summary>
public interface ITaskWriteRepository
{
        Task<TaskItem> AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(Guid id);
}

/// <summary>
/// Complete repository interface for Task
/// Aggregates read and write operations
/// </summary>
public interface ITaskRepository : ITaskReadRepository, ITaskWriteRepository;

/// <summary>
/// Read operations interface for Sprint repository
/// Follows Interface Segregation Principle
/// </summary>
public interface ISprintReadRepository
{
        Task<Sprint?> GetByIdAsync(Guid id);
        Task<IEnumerable<Sprint>> GetByProjectIdAsync(Guid projectId);
}

/// <summary>
/// Write operations interface for Sprint repository
/// Follows Interface Segregation Principle
/// </summary>
public interface ISprintWriteRepository
{
        Task<Sprint> AddAsync(Sprint sprint);
        Task UpdateAsync(Sprint sprint);
        Task DeleteAsync(Guid id);
}

/// <summary>
/// Complete repository interface for Sprint
/// Aggregates read and write operations
/// </summary>
public interface ISprintRepository : ISprintReadRepository, ISprintWriteRepository;
