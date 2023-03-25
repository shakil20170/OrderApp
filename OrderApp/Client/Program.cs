
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OrderApp.Client;
using OrderApp.Client.Services;
using OrderApp.Client.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7017/") });
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWindowService, WindowService>();
builder.Services.AddScoped<ISubElementService, SubElementService>();

await builder.Build().RunAsync();
