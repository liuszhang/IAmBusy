using IAmBusy.Model.ApiClient;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddHttpClient<MainApiClient>(client =>
{
    client.BaseAddress = new("http://localhost:5090");
    client.Timeout = TimeSpan.FromSeconds(60);
});

await builder.Build().RunAsync();
