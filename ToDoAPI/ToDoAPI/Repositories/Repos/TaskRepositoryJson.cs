using System.Text.Json;
using ToDoAPI.Entities;
using ToDoAPI.Repositories.Interfaces;

namespace ToDoAPI.Repositories.Repos
{
    public class TaskRepositoryJson : ITaskRepository
    {
        private readonly string _path;
        public TaskRepositoryJson()
        {
            _path = "tasks.json";
        }

        public async Task<IEnumerable<TodoTask>> GetAllAsync()
        {
            if (!File.Exists(_path)) 
                return new List<TodoTask>();

            var tasksJson = await File.ReadAllTextAsync(_path);

            return JsonSerializer.Deserialize<List<TodoTask>>(tasksJson) ?? [];
        }

        public async Task<TodoTask?> GetByIdAsync(int id)
        {
            var tasks = await GetAllAsync();

            return tasks.FirstOrDefault(x => x.Id == id);
        }

        public async Task CreateTaskAsync(TodoTask task)
        {
            var tasks = (await GetAllAsync()).ToList();

            task.Id = tasks.Any() ? tasks.Max(x => x.Id) + 1 : 1; // si hay tareas, toma el id mayor y sumale 1, si no hay, colocale id por defecto 1

            tasks.Add(task);

            await SaveAsync(tasks);
        }

        public async Task UpdateTaskAsync(int id, TodoTask task)
        {
            var tasks = (await GetAllAsync()).ToList();

            var taskPosition = tasks.FindIndex(x => x.Id == id); // si no existe, it returns -1

            if (taskPosition == -1)
                return;

            tasks[taskPosition] = task;

            await SaveAsync(tasks);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var tasks = (await GetAllAsync()).ToList();

            var taskToDelete = tasks.FirstOrDefault(x => x.Id == id);

            if (taskToDelete is null)
                return;

            tasks.Remove(taskToDelete);
            await SaveAsync(tasks);
        }

        private async Task SaveAsync(List<TodoTask> tasks)
        {
            var tasksJson = JsonSerializer.Serialize(tasks,new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(_path, tasksJson);
        }
    }
}
