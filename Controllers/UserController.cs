using Microsoft.AspNetCore.Mvc;
using Mvc.Services;
using Mvc.Models;
using Mvc.Repositories.Data.Entities;
using Mvc.Repositories.Interfaces;

namespace Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var getUser = await _userService.GetUserAsync();
            var viewModel = new UserMovieViewModel
            {
                GetUsers = getUser
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var movies = await _userService.GetUserAsync();
            return Json(movies);
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var data = await _userService.GetDataku();
            return Json(data);
        }
    }
}