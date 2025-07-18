//using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;



public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMainApiService(this IServiceCollection services)
        {
        services.AddDbContext<MainDbContext>(options =>
                options.UseSqlite("Data Source=main.db;Cache=Shared;"));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDepartmentManageService, DepartmentManageService>();
        services.AddScoped<ITaskManageService, TaskManageService>();


        return services;
        }

    public static IApplicationBuilder UseMainEndpointsManage(this IApplicationBuilder app)
    {
        
        return app.UseEndpoints(delegate (IEndpointRouteBuilder endpoints)
        {
            //endpoints.MapHub<MainHub>("/mainHub");
            endpoints.MapDepartmentManageApi();
            endpoints.MapTaskManageApi();
            endpoints.MapUserManageApi();

        });
    }

}

