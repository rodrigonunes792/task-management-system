namespace TaskManagement.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public ProjectStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();
    public ICollection<Sprint> Sprints { get; private set; } = new List<Sprint>();

    private Project() { } // EF Core

    public Project(string name, string description, DateTime startDate, DateTime? endDate = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Status = ProjectStatus.Active;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = ProjectStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = ProjectStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum ProjectStatus
{
    Active,
    Completed,
    Archived
}
