using IAmBusy.Model.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

public static class DepartmentManageApi
{
    public static IEndpointRouteBuilder MapDepartmentManageApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/department").WithTags("部门管理");

        //路由定义
        api.MapGet("/GetAllDepartments", async (IDepartmentManageService service) => await service.GetAllDepartmentsAsync());
        api.MapPost("/CreateDepartment", async (IDepartmentManageService service, [FromBody] Department newDepartment) => await service.CreateDepartmentAsync(newDepartment));
        api.MapDelete("/DeleteDepartment/{departmentId}", async (IDepartmentManageService service, int departmentId) => await service.DeleteDepartmentAsync(departmentId));




        return app;
    }

}

