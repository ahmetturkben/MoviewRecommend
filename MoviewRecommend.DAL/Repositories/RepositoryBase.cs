using Microsoft.EntityFrameworkCore;
using MoviewRecommend.DAL.Entities;
using MoviewRecommend.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MoviewRecommend.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseDALEntity
    {
        protected DbContext _context;
        private DbSet<TEntity> _dbSet;

        public abstract Expression<Func<TEntity, bool>> SearchFilters(TEntity obj);

        public void InitializeBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbSet.AsNoTracking().ToList();

            var query = _dbSet.Where(predicate);
            return query.AsNoTracking().ToList();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IList<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        //TODO: Tekbir field Update i için tüm objenin maplenmesine gerek yok. Böyle bir metot hazırlayabiliriz ek olarak.
        public virtual void Update(TEntity entity)
        {
            var local = _context.Set<TEntity>()
            .Local
            .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            _context.Entry(entity).State = EntityState.Modified;

            #region Bu mimaride geçerli olmayan state update yönetimleri!!
            //1
            //_dbSet.Update(entity);
            //_context.Entry<TEntity>(entity).State = EntityState.Modified;

            //2
            //this._dbSet.Attach(entity);
            //_context.Entry(newEntity).CurrentValues.SetValues(entity);
            //_context.Entry(entity).State = EntityState.Modified;
            #endregion
        }

        public DTO.RepositoryResult SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return new DTO.RepositoryResult(true, "İşlem Başarılı");
            }
            catch (Exception ex)
            {
                return new DTO.RepositoryResult(false, ex.InnerException.InnerException.Message);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            _dbSet = null;
            GC.SuppressFinalize(this);
        }
    }
}
