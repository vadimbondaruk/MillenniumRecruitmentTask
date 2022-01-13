using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MillenniumRecruitmentTask.Api.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity item, CancellationToken cancellationToken);

        Task<TEntity> FindByIdAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task RemoveAsync(TEntity item, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity item, CancellationToken cancellationToken);
    }
}
