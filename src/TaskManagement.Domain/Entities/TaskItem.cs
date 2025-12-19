namespace TaskManagement.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public TaskPriority Priority { get; private set; }
    public TaskStatus Status { get; private set; }
    public int EstimatedHours { get; private set; }
    public int ActualHours { get; private set; }
    public Guid ProjectId { get; private set; }
    public Project? Project { get; private set; }
    public Guid? SprintId { get; private set; }
    public Sprint? Sprint { get; private set; }
    public Guid? AssignedToId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private TaskItem() { } // EF Core

    public TaskItem(string title, string description, TaskPriority priority, int estimatedHours, Guid projectId)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Priority = priority;
        Status = TaskStatus.Todo;
        EstimatedHours = estimatedHours;
        ActualHours = 0;
        ProjectId = projectId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string title, string description, TaskPriority priority, int estimatedHours)
    {
        Title = title;
        Description = description;
        Priority = priority;
        EstimatedHours = estimatedHours;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTo(Guid userId)
    {
        AssignedToId = userId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(TaskStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void LogHours(int hours)
    {
        ActualHours += hours;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignToSprint(Guid sprintId)
    {
        SprintId = sprintId;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum TaskStatus
{
    Todo,
    InProgress,
    InReview,
    Done
}
