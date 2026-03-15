using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.DTOs;
using ToDoAPI.Enums;
using ToDoAPI.Services;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService service, ILogger<TaskController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> GetTasksAsync([FromQuery] TasksStatus? status)
        {
            _logger.LogInformation("Obteniendo tareas..");
            var tasks = await _service.GetTasksAsync(status);
            return Ok(tasks);
        }

        [HttpGet("{id:int}", Name = "GetTask")]
        public async Task<ActionResult<TaskResponse>> GetByIdAsync([FromRoute] int id)
        {
            _logger.LogInformation($"Obteniendo tarea id ({id})...");
            var task = await _service.GetByIdAsync(id);

            if (task is null)
                return NotFound();

            _logger.LogInformation($"Devolviendo tarea...");
            return task;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] CreateTaskRequest createTaskRequest)
        {
            try
            {
                var task = await _service.CreateTaskAsync(createTaskRequest);

                _logger.LogInformation($"Tarea creada");

                return CreatedAtRoute("GetTask", new { id = task.Id }, task);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Ha ocurrido un error creando nueva tarea");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTask(int id, [FromBody] UpdateTaskRequest updateTaskRequest)
        {
            try
            {
                var task = _service.GetByIdAsync(id);
                if (task is null)
                {
                    _logger.LogError($"No se encontro la tarea id ({id}) para actualizar");
                    return NotFound();
                }

                await _service.UpdateTaskAsync(id, updateTaskRequest);

                _logger.LogInformation($"Tarea id ({id}) actualizada");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error actualizando tarea");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                var task = _service.GetByIdAsync(id);
                if (task is null)
                {
                    _logger.LogError($"No se encontro la tarea id ({id}) para eliminar");
                    return NotFound();
                }

                await _service.DeleteTaskAsync(id);

                _logger.LogInformation($"Tarea id ({id}) borrada");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error eliminando tarea");
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id:int}/complete")]
        public async Task<ActionResult> CompleteTask(int id)
        {
            try
            {
                var task = await _service.GetByIdAsync(id);
                if (task is null)
                {
                    _logger.LogError($"No se encontro la tarea id ({id}) para completar");
                    return NotFound();
                }

                _logger.LogInformation("Tarea completada");
                await _service.CompleteTaskAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error completando tarea");
                return BadRequest(ex.Message);
            }
        }
    }
}
