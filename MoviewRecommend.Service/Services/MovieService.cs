using AutoMapper;
using MoviewRecommend.DAL.Interfaces;
using MoviewRecommend.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.Service.Services
{
    public class MovieService : ServiceBase<DAL.Entities.Movie, BLL.Movie>, IMovieService
    {
        public MovieService(IMapper mapper, IMovieRepository movieRepository) : base(mapper)
        {
            base.InitializeBase(movieRepository);
        }
    }
}
