using IAmBusy.Client.Pages;
using IAmBusy.Components;
using IAmBusy.Model.ApiClient;
using IAmBusy.Model.Models;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

builder.Services.AddMainApiService();

// ��� NSwag ����
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "Main API";
});

builder.Services.AddHttpClient<MainApiClient>(client =>
{
    client.BaseAddress = new("http://localhost:5090");
    client.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<MainDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.AllowedUserNameCharacters="";

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        // ����ͻ�����Դ����� Blazor �ͻ��˵�ַ��
        policy.WithOrigins("https://localhost:7186")
              // ������������ HTTP ������GET��POST �ȣ�
              .AllowAnyMethod()
              // ������������ HTTP ͷ���� Authorization��Content-Type��
              .AllowAnyHeader();
              // ����Я�����ƾ֤���� Cookie��Token����ѡ��
              //.AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowBlazorClient");

//app.UseHttpsRedirection();

app.UseOpenApi();
app.UseSwaggerUi();
//�����֤�м��
app.UseAuthentication();
// �����ȵ��� UseRouting
app.UseRouting();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(IAmBusy.Client._Imports).Assembly);

app.UseMainEndpointsManage();

app.Run();
