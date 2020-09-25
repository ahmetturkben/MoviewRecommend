using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoviewRecommend.Service.Interfaces
{
    public interface IService<TEntity, BLEntity>
    {
        BLEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        List<BLEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        void Add(BLEntity entity);
        void Update(BLEntity entity);
        void Remove(BLEntity entity);
        BLL.BusinessResult SaveChanges();
    }
}
