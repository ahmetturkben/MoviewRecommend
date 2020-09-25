using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviewRecommend.DAL.Interfaces;
using MoviewRecommend.DAL.Repositories;
using MoviewRecommend.Service.Interfaces;
using MoviewRecommend.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.Service.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static void Registrar(this IServiceCollection service, string conectionString)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IMovieService, MovieService>();
            service.AddScoped<INoteService, NoteService>();

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IMovieRepository, MovieRepository>();
            service.AddScoped<INoteRepository, NoteRepository>();

            service.AddDbContext<DAL.Infrastructure.AppDbContext>(op =>
            {
                op.UseLazyLoadingProxies(false); //lazy loading aktif edilmedi.
                op.UseSqlServer(conectionString);
                op.EnableSensitiveDataLogging();
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);
        }
    }
}
