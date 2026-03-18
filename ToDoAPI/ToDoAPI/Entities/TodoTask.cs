using System.ComponentModel.DataAnnotations;
using ToDoAPI.Enums;

namespace ToDoAPI.Entities
{
    public class TodoTask 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly DateLimit { get; set; }
        [EnumDataType(typeof(TasksStatus), ErrorMessage = "Error de estado invalido")]
        public TasksStatus Status { get; set; }
    }
}
