using MoviewRecommend.BLL.TheMovieDb;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoviewRecommend.Service.Interfaces
{
    public interface ITheMovieDbService
    {
        List<TheMovieDbModel> GetAll(Expression<Func<TheMovieDbModel, bool>> predicate = null);
    }
}
