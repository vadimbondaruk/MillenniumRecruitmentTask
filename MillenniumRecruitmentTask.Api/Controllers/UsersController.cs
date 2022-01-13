using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MillenniumRecruitmentTask.Api.Data.Entities;
using MillenniumRecruitmentTask.Api.Data.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MillenniumRecruitmentTask.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            this._userRepository = userRepository;
        }

        [HttpGet]
        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _userRepository.FindByIdAsync(id, cancellationToken);
        }
    }
}
