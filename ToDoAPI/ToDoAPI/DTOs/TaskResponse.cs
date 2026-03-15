using System.ComponentModel.DataAnnotations;
using ToDoAPI.Enums;

namespace ToDoAPI.DTOs
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly DateLimit { get; set; }
        public TasksStatus Status { get; set; }
    }
}
