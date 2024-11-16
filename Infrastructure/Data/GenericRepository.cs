using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T>(StoreContext storeContext) : IGenericRepository<T> where T : BaseEntities
    {
        public void Add(T entity)
        {
            storeContext.Set<T>().Add(entity);
        }

        public bool Exists(int id)
        {
            return storeContext.Set<T>().Any(x => x.Id == id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await storeContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetEntityWithSpec(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }

        public async  Task<TResult?> GetEntityWithSpec<TResult>(Ispecification<T, TResult> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await storeContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }

        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(Ispecification<T, TResult> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }

        public void Remove(T entity)
        {
            storeContext.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await storeContext.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            storeContext.Set<T>().Attach(entity);
            storeContext.Entry(entity).State = EntityState.Modified;
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvaluator<T>.GetQuery(storeContext.Set<T>().AsQueryable(), Spec);
        }
        private IQueryable<TResult> ApplySpecification<TResult>(Ispecification<T,TResult> Spec)
        {
            return SpecificationEvaluator<T>.GetQuery<T,TResult>(storeContext.Set<T>().AsQueryable(), Spec);
        }
    }
}
