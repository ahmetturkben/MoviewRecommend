using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoviewRecommend.DAL.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IList<TEntity> entities);
        void Update(TEntity entity);
        DTO.RepositoryResult SaveChanges();
        void Dispose();
        Expression<Func<TEntity, bool>> SearchFilters(TEntity obj);
    }
}
