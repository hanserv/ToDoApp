using ToDoAPI.DTOs;
using ToDoAPI.Entities;

namespace ToDoAPI.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TodoTask>> GetAllAsync();
        Task<TodoTask?> GetByIdAsync(int id);
        Task CreateTaskAsync(TodoTask task);
        Task UpdateTaskAsync(int id, TodoTask task);
        Task DeleteTaskAsync(int id);
    }
}