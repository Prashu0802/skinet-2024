﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface IGenericRepository<T> where T : BaseEntities
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T?> GetEntityWithSpec(ISpecification<T> Spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> Spec);
        Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> Spec);
        Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> Spec);
        void Add (T entity);
        void Update (T entity);
        void Remove (T entity);
        Task<bool> SaveAllAsync();
        bool Exists(int id);
        Task<int> CountAsync(ISpecification<T> Spec);
    }
}
