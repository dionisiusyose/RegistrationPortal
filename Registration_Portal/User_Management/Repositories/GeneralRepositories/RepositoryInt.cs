using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Management.Bases;
using User_Management.Context;
using User_Management.Repositories.Interfaces;

namespace User_Management.Repositories.GeneralRepositories
{
    public class RepositoryInt<TEntity, IContext> : IRepositoryInt<TEntity>
        where TEntity : class, IEntityInt
        where IContext : MyContext
    {
        private readonly MyContext _myContext;

        public RepositoryInt(MyContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                return entity;
            }
            _myContext.Set<TEntity>().Remove(entity);
            await _myContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> Get()
        {
            return await _myContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _myContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Post(TEntity entity)
        {
            await _myContext.Set<TEntity>().AddAsync(entity);
            await _myContext.SaveChangesAsync();
            return entity;

        }

        public async Task<TEntity> Put(TEntity entity)
        {
            _myContext.Entry(entity).State = EntityState.Modified;
            await _myContext.SaveChangesAsync();
            return entity;
        }
    }
}
