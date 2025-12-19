using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Sprint> Sprints { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            
            entity.HasMany(e => e.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Sprints)
                .WithOne(s => s.Project)
                .HasForeignKey(s => s.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);

            entity.HasOne(e => e.Sprint)
                .WithMany(s => s.Tasks)
                .HasForeignKey(e => e.SprintId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Goal).HasMaxLength(500);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var projectId = Guid.NewGuid();
        var sprintId = Guid.NewGuid();

        modelBuilder.Entity<Project>().HasData(
            new
            {
                Id = projectId,
                Name = "E-Commerce Platform",
                Description = "Build a new e-commerce platform",
                StartDate = DateTime.UtcNow,
                EndDate = (DateTime?)null,
                Status = ProjectStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Sprint>().HasData(
            new
            {
                Id = sprintId,
                Name = "Sprint 1",
                Goal = "Implement user authentication",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(14),
                Status = SprintStatus.Active,
                ProjectId = projectId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<TaskItem>().HasData(
            new
            {
                Id = Guid.NewGuid(),
                Title = "Implement user login",
                Description = "Create login functionality with JWT",
                Priority = TaskPriority.High,
                Status = TaskManagement.Domain.Entities.TaskStatus.InProgress,
                EstimatedHours = 8,
                ActualHours = 4,
                ProjectId = projectId,
                SprintId = (Guid?)sprintId,
                AssignedToId = (Guid?)null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new
            {
                Id = Guid.NewGuid(),
                Title = "Create user registration",
                Description = "Implement user registration with validation",
                Priority = TaskPriority.High,
                Status = TaskManagement.Domain.Entities.TaskStatus.Todo,
                EstimatedHours = 6,
                ActualHours = 0,
                ProjectId = projectId,
                SprintId = (Guid?)sprintId,
                AssignedToId = (Guid?)null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
