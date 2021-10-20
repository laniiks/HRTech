﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HRTech.Application.Abstractions
{
    public interface IRepository<T>  where T: class 
    {
        Task<T> GetById(int id, CancellationToken cancellationToken);
        Task<ICollection<T>> GetAll(CancellationToken cancellationToken);
        Task<int> GetCount(CancellationToken cancellationToken);
        Task<T> Add(T entity, CancellationToken cancellationToken);
        Task<T> Update(T entity, CancellationToken cancellationToken);
        Task Delete(T entity, CancellationToken cancellationToken);
        Task SaveChanges(CancellationToken cancellationToken);
    }
}