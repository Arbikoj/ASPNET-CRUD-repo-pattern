
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.IO;
using Mvc.Repositories.Data.Entities;
using Mvc.Repositories.Interfaces;

namespace Mvc.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MovieService> _logger;

        public MovieService(
            IUnitOfWork unitOfWork,
            ILogger<MovieService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> AddMovieAsync(Movies movie)
        {
            return await _unitOfWork.MovieRepository.AddMovieAsync(movie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            return await _unitOfWork.MovieRepository.DeleteMovieAsync(id);
        }

        public async Task<IEnumerable<Movies>> GetMovieByIdAsync(int id)
        {
            return await _unitOfWork.MovieRepository.GetMovieByIdAsync(id);
        }

        public async Task<IEnumerable<Movies>> GetMoviesAsync()
        {
            return await _unitOfWork.MovieRepository.GetMoviesAsync();
        }

        public async Task<bool> UpdateMovieAsync(Movies movie)
        {
            return await _unitOfWork.MovieRepository.UpdateMovieAsync(movie);
        }

    }
}