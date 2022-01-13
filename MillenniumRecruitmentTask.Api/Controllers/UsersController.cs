using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MillenniumRecruitmentTask.Api.Data.Entities;
using MillenniumRecruitmentTask.Api.Data.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
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
        public async Task<IActionResult> CreateAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("name can not be empty");
            }
            var users = await _userRepository.GetAllAsync(cancellationToken);

            var maxId = users.ToList().Select(x => x.Id).Max();

            await _userRepository.CreateAsync(new Data.Entities.User {Id = ++maxId, Name = name }, cancellationToken);

            return Ok();
        }

        // TODO: replace user by view model 

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("name can not be empty");

            }

            var existedUser = await _userRepository.FindByIdAsync(user.Id, cancellationToken);

            if(existedUser == null)
            {
                return NotFound(user);
            }

            await _userRepository.UpdateAsync(user, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existedUser = await _userRepository.FindByIdAsync(id, cancellationToken);

            if (existedUser == null)
            {
                return NotFound(id);
            }

            await _userRepository.RemoveAsync(new Data.Entities.User { Id = id }, cancellationToken);

            return Ok();
        }
    }
}
