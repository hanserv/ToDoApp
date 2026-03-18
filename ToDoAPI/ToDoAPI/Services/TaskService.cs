using ToDoAPI.DTOs;
using ToDoAPI.Entities;
using ToDoAPI.Enums;
using ToDoAPI.Repositories.Interfaces;

namespace ToDoAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskResponse>> GetTasksAsync(TasksStatus? status)
        {
            var tasks = await _repository.GetAllAsync();

            if (status is not null)
                tasks = tasks.Where(t => t.Status == status);

            return tasks.Select(task => new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                DateLimit = task.DateLimit,
                Status = task.Status
            });
        }

        public async Task<TaskResponse?> GetByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);

            if(task is null)
                return null;

            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                DateLimit = task.DateLimit,
                Status = task.Status
            };
        }

        public async Task<TaskResponse> CreateTaskAsync(CreateTaskRequest createTaskRequest)
        {
            if (createTaskRequest.DateLimit < DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("La fecha limite no debe ser pasada");

            var task = new TodoTask
            {
                Title = createTaskRequest.Title,
                Description = createTaskRequest.Description,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                DateLimit = createTaskRequest.DateLimit,
                Status = TasksStatus.Pending
            };

            await _repository.CreateTaskAsync(task);

            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                DateLimit = task.DateLimit,
                Status = task.Status
            };
        }

        public async Task UpdateTaskAsync(int id, UpdateTaskRequest updateTaskRequest)
        {
            if (updateTaskRequest.DateLimit < DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("La fecha limite no debe ser pasada");

            var task = await _repository.GetByIdAsync(id);

            if (task.Status == TasksStatus.Completed)
                throw new Exception("No se puede actualizar una tarea completada");

            if (task is null)
                throw new ArgumentException("La tarea no existe");

            task.Title = updateTaskRequest.Title;
            task.Description = updateTaskRequest.Description;
            task.DateLimit = updateTaskRequest.DateLimit;

            await _repository.UpdateTaskAsync(id,task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task is null)
                throw new ArgumentException("La tarea no existe");

            await _repository.DeleteTaskAsync(id);
        }

        public async Task CompleteTaskAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task is null)
                throw new ArgumentException("La tarea no existe");

            if (task.Status == TasksStatus.Completed)
                throw new InvalidOperationException("La tarea ya esta completada");

            task.Status = TasksStatus.Completed;
            await _repository.UpdateTaskAsync(id, task);
        }
    }
}
