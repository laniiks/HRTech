using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HRTech.Infrastructure.DataAccess.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext _databaseContext;
        protected readonly DbSet<T> _entity;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _entity = _databaseContext.Set<T>();
        }

        public async Task<T> GetById(int id, CancellationToken cancellationToken)
        {
            return await _entity.FindAsync(id);
        }
        
        public async Task<T> GetByIdGuid(Guid id)
        {
            return await _entity.FindAsync(id);
        }
        public async Task<T> GetById(string id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<ICollection<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _entity.ToArrayAsync(cancellationToken: cancellationToken);
        }

        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
            return await _entity.CountAsync(cancellationToken: cancellationToken);
        }

        public async Task<T> Add(T entity, CancellationToken cancellationToken)
        {
            await _entity.AddAsync(entity, cancellationToken);
            return entity;        
        }

        public async Task<T> Update(T entity, CancellationToken cancellationToken)
        {
            _entity.Update(entity);
            return entity;
        }

        public Task Delete(T entity, CancellationToken cancellationToken)
        {
            _entity.Remove(entity);
            return _databaseContext.SaveChangesAsync(cancellationToken);        
        }

        public async Task SaveChanges(CancellationToken cancellationToken)
        {
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}