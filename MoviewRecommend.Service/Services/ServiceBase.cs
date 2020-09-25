using AutoMapper;
using MoviewRecommend.DAL.Interfaces;
using MoviewRecommend.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoviewRecommend.Service.Services
{
    public class ServiceBase<TEntity, BLEntity> : IService<TEntity, BLEntity>
        where TEntity : class
        where BLEntity : BLL.BaseEntity.EntityBase
    {
        //E Entity;
        protected IRepositoryBase<TEntity> repo;
        protected readonly IMapper _mapper;

        public ServiceBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void InitializeBase(IRepositoryBase<TEntity> myRepo)
        {
            repo = myRepo;
        }

        public BLEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _mapper.Map<BLEntity>(repo.GetSingle(predicate));
        }
        public List<BLEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if(predicate == null)
                return _mapper.Map<List<BLEntity>>(repo.GetAll());
            return _mapper.Map<List<BLEntity>>(repo.GetAll(predicate));
        }

        // Start: Create Methods
        public void Add(BLEntity entity)
        {
            SetCreatedDetail(entity);
            repo.Add(_mapper.Map<TEntity>(entity));
        }
        public void Update(BLEntity entity)
        {
            SetModifiedDetail(entity);
            TEntity ent = _mapper.Map<TEntity>(entity);
            //TEntity tEntity = repo.GetSingle(repo.SearchFilters(ent));
            //repo.Update(ent, tEntity);
            repo.Update(ent);
        }

        public void Remove(BLEntity entity)
        {
            SetIsDeletedToTrue(entity);
            repo.Update(_mapper.Map<TEntity>(entity)/*, _mapper.Map<TEntity>(entity)*/);
        }

        public BLL.BusinessResult SaveChanges()
        {
            var result = new BLL.BusinessResult(repo.SaveChanges());

            // Error occured
            if (!result.Result)
            {
                result.Message = "Bir hata oluştu. Sistem yönetinize başvurun.";
            }

            return result;
        }

        public void Dispose()
        {
            repo.Dispose();
            GC.SuppressFinalize(this);
        }


        private void SetCreatedDetail(BLL.BaseEntity.IEntityBase BLEntity)
        {
            BLEntity.SetCreatedDetail();
        }

        private void SetModifiedDetail(BLL.BaseEntity.IEntityBase BLEntity)
        {
            BLEntity.SetModifiedDetail();
        }

        private void SetIsDeletedToTrue(BLL.BaseEntity.IEntityBase BLEntity)
        {
            BLEntity.SetIsDeletedToTrue();
        }
    }
}
