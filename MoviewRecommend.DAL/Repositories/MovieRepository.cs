using MoviewRecommend.DAL.Entities;
using MoviewRecommend.DAL.Infrastructure;
using MoviewRecommend.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoviewRecommend.DAL.Repositories
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext context)
        {
            base.InitializeBase(context);
        }

        public override Expression<Func<Movie, bool>> SearchFilters(Movie obj)
        {
            return (x => x.Id == obj.Id);
        }
    }
}
