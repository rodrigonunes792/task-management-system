using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Validators;

/// <summary>
/// Validator for Project entity
/// Implements Single Responsibility Principle - focused only on validation logic
/// Follows Strategy pattern for pluggable validation
/// </summary>
public interface IProjectValidator
{
      ValidationResult Validate(Project project);
}

/// <summary>
/// Default implementation of Project validator
/// Encapsulates all project validation logic in a dedicated service
/// </summary>
public class ProjectValidator : IProjectValidator
{
    public ValidationResult Validate(Project project)
        {
                var errors = new List<string>();

                                if (string.IsNullOrWhiteSpace(project.Name))
                                            errors.Add("Project name is required");

                                                                if (project.Name?.Length > 255)
                                                                            errors.Add("Project name must not exceed 255 characters");

                                                                                                if (project.StartDate > project.EndDate && project.EndDate.HasValue)
                                                                                                            errors.Add("Start date must be before end date");
                                                                                                                        
                                                                                                                                if (project.StartDate < DateTime.UtcNow.Date)
                                                                                                                                            errors.Add("Start date cannot be in the past");
                                                                                                                                                    
                                                                                                                                                            return new ValidationResult(errors);
                                                                                                                                                                }
                                                                                                                                                                }
                                                                                                                                                                
/// <summary>
/// Validator for TaskItem entity  
/// Implements Single Responsibility Principle - focused only on validation logic
/// </summary>
public interface ITaskItemValidator
{
      ValidationResult Validate(TaskItem taskItem);
}

/// <summary>
/// Default implementation of TaskItem validator
/// Encapsulates all task validation logic in a dedicated service
/// </summary>
public class TaskItemValidator : ITaskItemValidator
{
    public ValidationResult Validate(TaskItem taskItem)
        {
                var errors = new List<string>();

                                if (string.IsNullOrWhiteSpace(taskItem.Title))
                                            errors.Add("Task title is required");

                                                                if (taskItem.Title?.Length > 200)
                                                                            errors.Add("Task title must not exceed 200 characters");

                                                                                                if (taskItem.EstimatedHours <= 0)
                                                                                                            errors.Add("Estimated hours must be greater than zero");
                                                                                                                        
                                                                                                                                if (taskItem.ActualHours < 0)
                                                                                                                                            errors.Add("Actual hours cannot be negative");
                                                                                                                                                    
                                                                                                                                                            return new ValidationResult(errors);
                                                                                                                                                                }
                                                                                                                                                                }
                                                                                                                                                                
/// <summary>
/// Encapsulates validation results
/// </summary>
public class ValidationResult
{
      public ValidationResult(List<string> errors)
      {
                Errors = errors;
      }

      public bool IsValid => !Errors.Any();
      public List<string> Errors { get; }
}
