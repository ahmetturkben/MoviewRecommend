using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.Service.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DAL.Entities.User, BLL.User>().ReverseMap();
            CreateMap<DAL.Entities.Movie, BLL.Movie>().ReverseMap();
            CreateMap<DAL.Entities.Note, BLL.Note>().ReverseMap();
        }
    }
}
