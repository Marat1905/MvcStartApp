using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Interfaces;
using MvcStartApp.Models.DB;

namespace MvcStartApp.Controllers
{
    public class UsersController:Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IBlogRepository _repo;

        public UsersController(ILogger<UsersController> logger, IBlogRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            await _repo.AddUser(newUser);
            return View(newUser);
        }
    }
}
