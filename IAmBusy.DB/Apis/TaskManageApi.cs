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
        //api.MapGet("/getPDZById/{PDZId}", async (ITaskManageService service,string PDZId) => await service.GetPdzById(PDZId));
        //api.MapPost("/getPdzByFilter", async (ITaskManageService service, [FromBody] PDZFilter filter) => await service.GetByFilter(filter));
        api.MapPost("/CreateTask", async (ITaskManageService service, [FromBody] UserTask Task) => await service.CreateTask(Task));
        //api.MapPut("/updatePDZ", async (ITaskManageService service, [FromBody] PlugDataZone pdz) => await service.UpdatePDZ(pdz));
        api.MapDelete("/DeleteTask/{TaskId}", async (ITaskManageService service, int TaskId) => await service.DeleteTask(TaskId));
        //api.MapPost("/deleteByFilter", async (ITaskManageService service, [FromBody] PDZFilter filter) => await service.DeleteByFilter(filter));


        return app;
    }

}

