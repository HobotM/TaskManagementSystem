namespace TaskManagementSystem.Models;
/// <summary>
/// Represents a task that belongs to a user.
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public string UserId { get; set; } = default!;
}