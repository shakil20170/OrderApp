using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using OrderApp.Server.Data;
using OrderApp.Server.Repositories;
using OrderApp.Server.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OrderAppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("OrderAppCon")));
builder.Services.AddCors(cores => cores.AddPolicy("MyPolicy", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IWindowRepository,WindowRepository>();
builder.Services.AddScoped<ISubElementRepository,SubElementRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
