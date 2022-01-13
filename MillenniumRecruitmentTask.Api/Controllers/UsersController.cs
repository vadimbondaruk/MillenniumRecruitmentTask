using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MillenniumRecruitmentTask.Api.Data.Entities;
using MillenniumRecruitmentTask.Api.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("id:int")]
        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _userRepository.FindByIdAsync(id, cancellationToken);
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }

        [HttpPost]
        public async Task CreateAsync(string name, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);

            var maxId = users.ToList().Select(x => x.Id).Max();

            await _userRepository.CreateAsync(new Data.Entities.User {Id = ++maxId, Name = name }, cancellationToken);
        }

        // TODO: replace user by view model 

        [HttpPut]
        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        [HttpDelete]
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _userRepository.RemoveAsync(new Data.Entities.User { Id = id }, cancellationToken);
        }
    }
}
