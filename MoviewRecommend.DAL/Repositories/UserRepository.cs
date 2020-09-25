using MoviewRecommend.DAL.Entities;
using MoviewRecommend.DAL.Infrastructure;
using MoviewRecommend.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoviewRecommend.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext context)
        {
            base.InitializeBase(context);
        }

        public override Expression<Func<User, bool>> SearchFilters(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
