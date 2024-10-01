
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.IO;
using Mvc.Repositories.Data.Entities;
using Mvc.Repositories.Interfaces;

namespace Mvc.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<UserService> _logger;

        public UserService(
            IUnitOfWork unitOfWork,
            ILogger<UserService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Users>> GetDataku()
        {
            return await _unitOfWork.UserRepository.GetDataku();
        }

        public async Task<IEnumerable<Users>> GetUserAsync()
        {
            return await _unitOfWork.UserRepository.GetUserAsync();
        }
    }
}