using Microsoft.EntityFrameworkCore;
using MvcStartApp.Context;
using MvcStartApp.Interfaces;
using MvcStartApp.Middlewares;
using MvcStartApp.Repositories;


var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

// Build the intermediate service provider
var sp = builder.Services.BuildServiceProvider();
// This will succeed.
var config1 = sp.GetService<BlogContext>();

// регистрация сервиса репозитория для взаимодействия с базой данных
builder.Services.AddTransient<IBlogRepository, BlogRepository>();

builder.Services.AddSingleton<IRequestsRepository,RequestsRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Подключаем логирвоание с использованием ПО промежуточного слоя
app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
