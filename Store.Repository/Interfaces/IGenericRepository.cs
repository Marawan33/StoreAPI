using Store.Data.Entities;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey? id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs);
        Task<TEntity> GetByIdWithSpeceficationAsync(ISpecification<TEntity> specs);
        Task AddAsync(TEntity entity);
        Task<int> GetCountWithSpecification(ISpecification<TEntity> specs);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
