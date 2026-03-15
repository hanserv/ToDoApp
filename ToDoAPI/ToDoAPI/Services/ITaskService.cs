using ToDoAPI.DTOs;
using ToDoAPI.Enums;

namespace ToDoAPI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponse>> GetTasksAsync(TasksStatus? status);
        Task<TaskResponse?> GetByIdAsync(int id);
        Task<TaskResponse> CreateTaskAsync(CreateTaskRequest createTaskRequest);
        Task UpdateTaskAsync(int id,UpdateTaskRequest updateTaskRequest);
        Task CompleteTaskAsync(int id);
        Task DeleteTaskAsync(int id);
    }
}