namespace TaskManagementSystem.Models
{
    /// <summary>
    /// Represents a task that belongs to a user.
    /// </summary>
    public class TaskDto
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        
    }
}   