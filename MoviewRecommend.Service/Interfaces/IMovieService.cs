using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.Service.Interfaces
{
    public interface IMovieService : IService<DAL.Entities.Movie, BLL.Movie>
    {
    }
}
