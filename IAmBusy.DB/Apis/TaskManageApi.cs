using IAmBusy.Model.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

public static class TaskManageApi
{
    public static IEndpointRouteBuilder MapTaskManageApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/task").WithTags("任务管理");

        api.MapGet("/getAllTasks", async (ITaskManageService service) => await service.GetAllTasks());
        api.MapPost("/CreateTask", async (ITaskManageService service, [FromBody] UserTask Task) => await service.CreateTask(Task));
        api.MapDelete("/DeleteTask/{TaskId}", async (ITaskManageService service, int TaskId) => await service.DeleteTask(TaskId));

        return app;
    }

}

