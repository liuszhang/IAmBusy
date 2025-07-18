using IAmBusy.Model.Models;
using Microsoft.EntityFrameworkCore;

public class TaskManageService : ITaskManageService
{
    private readonly MainDbContext _dbContext;
    public TaskManageService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task<UserTask?> CreateTask(UserTask Task, CancellationToken cancellationToken = default)
    {
        _dbContext.UserTasks.Add(Task);
        try
        {
            await _dbContext.SaveChangesAsync();
            return Task;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteTask(int TaskId, CancellationToken cancellationToken = default)
    {
        var Task = await _dbContext.UserTasks.FindAsync(TaskId);
        if (Task == null)
        {
            return false;
        }

        _dbContext.UserTasks.Remove(Task);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserTask>?> GetAllTasks(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbContext.UserTasks.Include(t=>t.User).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
}

