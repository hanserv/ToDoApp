using System.ComponentModel.DataAnnotations;
using ToDoAPI.Enums;

namespace ToDoAPI.DTOs
{
    public class UpdateTaskRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public required string Title { get; set; }
        [StringLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string? Description { get; set; }
        public DateOnly DateLimit { get; set; }
    }
}
