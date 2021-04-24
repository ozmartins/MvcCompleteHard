using Hard.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity 
    {
        Task Create(TEntity entity);

        Task<TEntity> Recover(Guid id);

        Task<List<TEntity>> RecoverAll();

        Task Update(TEntity entity);

        Task Delete(Guid id);

        Task<IEnumerable<TEntity>>  Search(Expression<Func<TEntity, bool>> predicate);
    }
}
