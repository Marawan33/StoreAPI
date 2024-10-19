﻿using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public class SpecificationEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> specs)
        {
            var query = inputQuery;
            if(specs.Criteria is not null)
            {
                query = query.Where(specs.Criteria);
            }
            query = specs.Includes.Aggregate(query,(current,includeEx)=>current.Include(includeEx)); 
            return query;
        } 
    }
}