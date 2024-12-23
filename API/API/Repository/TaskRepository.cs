using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskEntity> GetTaskByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<TaskEntity> CreateTaskAsync(TaskEntity task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateTaskAsync(TaskEntity task)
    {
        _context.Entry(task).State = EntityState.Modified;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}

