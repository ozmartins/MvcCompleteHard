using Hard.Business.Interfaces;
using Hard.Business.Models;
using Hard.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hard.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly HardDbContext Context;

        protected readonly DbSet<TEntity> DbSet;

        public Repository(HardDbContext Context)
        {
            this.Context = Context;
            this.DbSet = this.Context.Set<TEntity>();
        }

        public async Task Create(TEntity entity)
        {
            DbSet.Add(entity);
            await this._saveChanges();
        }
        public async Task<TEntity> Recover(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<List<TEntity>> RecoverAll()
        {
            return await DbSet.ToListAsync();
        }
        public async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await this._saveChanges();
        }        
        public async Task Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await _saveChanges();
        }                
        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        private async Task<int> _saveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
