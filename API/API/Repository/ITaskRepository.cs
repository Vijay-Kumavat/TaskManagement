using API.Models;

namespace API.Repository;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllTasksAsync();
    Task<TaskEntity> GetTaskByIdAsync(int id);
    Task<TaskEntity> CreateTaskAsync(TaskEntity task);
    Task<bool> UpdateTaskAsync(TaskEntity task);
    Task<bool> DeleteTaskAsync(int id);
}

