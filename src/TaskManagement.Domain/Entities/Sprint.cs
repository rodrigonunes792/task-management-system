namespace TaskManagement.Domain.Entities;

public class Sprint
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Goal { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public SprintStatus Status { get; private set; }
    public Guid ProjectId { get; private set; }
    public Project? Project { get; private set; }
    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Sprint() { } // EF Core

    public Sprint(string name, string goal, DateTime startDate, DateTime endDate, Guid projectId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Goal = goal;
        StartDate = startDate;
        EndDate = endDate;
        Status = SprintStatus.Planned;
        ProjectId = projectId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Start()
    {
        if (Status != SprintStatus.Planned)
            throw new InvalidOperationException("Only planned sprints can be started");

        Status = SprintStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status != SprintStatus.Active)
            throw new InvalidOperationException("Only active sprints can be completed");

        Status = SprintStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum SprintStatus
{
    Planned,
    Active,
    Completed
}
