using IAmBusy.Model.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

public static class UserManageApi
{
    public static IEndpointRouteBuilder MapUserManageApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/user").WithTags("用户管理");

        api.MapGet("/getAllUsers", async (IUserService service) => await service.GetAllUsers());
        api.MapPost("/CreateUser", async (IUserService service, [FromBody] User newUser) => await service.RegisterUserAsync(newUser));
        api.MapDelete("/DeleteUser/{userId}", async (IUserService service, int userId) => await service.DeleteUserAsync(userId));



        api.MapPost("/sigup", async (IUserService service, User request) => await service.RegisterUserAsync(request));
        api.MapPost("/sigin", async (IUserService service, User request) => await service.LoginUserAsync(request));
        api.MapGet("/logout/{userId}", async (IUserService service, string userId) => await service.LogoutUserAsync(userId));



        return app;
    }

}
