using MvcStartApp.Interfaces;
using MvcStartApp.Models.DB;

namespace MvcStartApp.Middlewares;
public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestsRepository _repo;

    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next, IRequestsRepository repo)
    {
        _next = next;
        _repo = repo;
    }

    /// <summary>
    ///  Необходимо реализовать метод Invoke  или InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        Request user = new Request() { Date = DateTime.Now, Url =$"http://{context.Request.Host.Value + context.Request.Path} " };
        await _repo.AddRequestsAsync(user);

        LogConsole(context);
        await LogFileAsync(context);

        // Передача запроса далее по конвейеру
        await _next.Invoke(context);
    }

    private void LogConsole(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }

        private async Task LogFileAsync(HttpContext context)
        {
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");

            // Используем асинхронную запись в файл
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }
    }
