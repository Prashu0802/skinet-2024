using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntities
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> Spec)
        {
            if(Spec.Criteria!=null)
            {
                query=query.Where(Spec.Criteria);
            }

            if (Spec.OrderBy != null)
            {
                query = query.OrderBy(Spec.OrderBy);
            }

            if (Spec.OrderByDesending != null)
            {
                query = query.OrderByDescending(Spec.OrderByDesending);
            }

            if (Spec.IsDistinct)
            {
                query = query.Distinct();
            }

            if (Spec.IsPagingEnabled)
            {
                query = query.Skip(Spec.Skip).Take(Spec.Take);
            }
            //if (Spec.Criteria != null)
            //{
            //    query = query.Where(Spec.Criteria);
            //}
            return query;
        }
        public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> query, ISpecification<T,TResult> Spec)
        {
            if (Spec.Criteria != null)
            {
                query = query.Where(Spec.Criteria);
            }

            if (Spec.OrderBy != null)
            {
                query = query.OrderBy(Spec.OrderBy);
            }

            if (Spec.OrderByDesending != null)
            {
                query = query.OrderByDescending(Spec.OrderByDesending);
            }

            var selectQuery = query as IQueryable<TResult>;

            if (Spec.Select != null)
            {
                selectQuery=query.Select(Spec.Select);
            }
            if (Spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }
            if (Spec.IsPagingEnabled)
            {
                selectQuery = selectQuery?.Skip(Spec.Skip).Take(Spec.Take);
            }

            return selectQuery ?? query.Cast<TResult>();
        }
    }
}

