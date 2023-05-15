﻿using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GettAllAsync();

        Task<IEnumerable<T>> GettAllAsync(params Expression<Func<T, object>>[] includePropierties);

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(int id, T entity);
        
        Task DeleteAsync(int id);
    }
}
