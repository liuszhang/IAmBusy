using IAmBusy.Model.Models;

public interface ITaskManageService
{

    Task<List<UserTask>?> GetAllTasks(CancellationToken cancellationToken = default);
    Task<UserTask?> CreateTask(UserTask Task, CancellationToken cancellationToken = default);
    Task<bool> DeleteTask(int TaskId, CancellationToken cancellationToken = default);
}