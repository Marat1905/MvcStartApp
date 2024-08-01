using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Interfaces;

namespace MvcStartApp.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogger<LogsController> _logger;
        private readonly IRequestsRepository _repo;

        public LogsController(ILogger<LogsController> logger, IRequestsRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

      
        public async Task<IActionResult> Index()
        {
            var requests = await _repo.GetAllRequestsAsync();
            return View(requests);
        }
    }
}
