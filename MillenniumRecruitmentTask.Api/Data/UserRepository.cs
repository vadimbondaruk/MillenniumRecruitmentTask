using Microsoft.Extensions.Logging;
using MillenniumRecruitmentTask.Api.Data.Entities;
using MillenniumRecruitmentTask.Api.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MillenniumRecruitmentTask.Api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IStore _store;
        private readonly ILogger<UserRepository> _logger;


        public UserRepository(IStore store, ILogger<UserRepository> logger)
        {
            _store = store;
            _logger = logger;
        }

        public Task CreateAsync(User item, CancellationToken cancellationToken)
        {
            _store.Users.Add(item);
            return Task.CompletedTask;
        }

        public Task<User> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            return Task.FromResult(_store.Users.FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_store.Users.AsEnumerable());
        }

        public Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.FromResult(_store.Users.Where(predicate.Compile()));
        }

        public Task RemoveAsync(User item, CancellationToken cancellationToken)
        {
            var user = _store.Users.FirstOrDefault(x => x.Id == item.Id);
            if (user != null)
            {
                _store.Users.Remove(user); 
            }

            return Task.CompletedTask;
        }

        public Task UpdateAsumc(User item, CancellationToken cancellationToken)
        {
            var user = _store.Users.FirstOrDefault(x => x.Id == item.Id);
            if (user != null)
            {
                user.Name = item.Name;
            }

            return Task.CompletedTask;
        }
    }
}
