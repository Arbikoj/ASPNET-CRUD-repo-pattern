using Microsoft.AspNetCore.Mvc;
using Mvc.Services;
using Mvc.Models;
using Mvc.Repositories.Data.Entities;
using Mvc.Repositories.Interfaces;

namespace Mvc.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;

        public MovieController(ILogger<MovieController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var getMovie = await _movieService.GetMoviesAsync();
            var viewModel = new MovieViewModel
            {
                GetMovies = getMovie
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetMoviesAsync();
            return Json(movies);
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid movie ID." });
            }
            var movie = await _movieService.GetMovieByIdAsync(id);

            if (!movie.Any())
            {
                return NotFound(new { success = false, message = "Movie not found." });
            }

            return Ok(new { success = true, data = movie.FirstOrDefault() });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateMovies(Movies movie)
        {
            if (movie == null || movie.Id == 0)
            {
                return BadRequest(new { success = false, message = "Invalid movie data." });
            }

            var result =await _movieService.UpdateMovieAsync(movie);

            if (result)
            {
                return Ok(new { success = true, message = "Movie updated successfully." });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Error updating movie." });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovies(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid movie ID." });
            }

            var result = await _movieService.DeleteMovieAsync(id);

            if (result)
            {
                return Ok(new { success = true, message = "Movie deleted successfully." });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Error deleting movie." });
        }

        [HttpPost]
        public async Task<IActionResult> AddMovies([FromBody] Movies movie)
        {
            if (movie == null || string.IsNullOrEmpty(movie.nama) || string.IsNullOrEmpty(movie.genre))
            {
                return BadRequest(new { success = false, message = "Invalid movie data." });
            }

            var result = await _movieService.AddMovieAsync(movie);

            if (result)
            {
                return Ok(new { success = true, message = "Movie added successfully." });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Error adding movie." });
        }
    }
}