using Microsoft.Extensions.Options;
using MoviewRecommend.BLL;
using MoviewRecommend.BLL.TheMovieDb;
using MoviewRecommend.Service.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;

namespace MoviewRecommend.Service.Services
{
    public class TheMovieDbService : ITheMovieDbService
    {
        public const int total_movie = 500;

        private readonly AppSettings _appSettings;
        public TheMovieDbService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public List<TheMovie> GetAll(Expression<Func<TheMovieDbModel, bool>> predicate = null)
        {
            var list = new List<TheMovie>();

            var client = new RestClient("https://api.themoviedb.org/3");
            var request = new RestRequest("/trending/all/week", Method.GET);
            request.AddParameter("api_key", _appSettings.TheMovieDbSecretKey);
            request.AddHeader("content-type", "application/json");

            int page = 1;
            for (int i = 0; i <= page; i++)
            {
                request.AddOrUpdateParameter("page", page);
                var result = client.Execute(request);

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                var data = JsonConvert.DeserializeObject<TheMovieDbModel>(result.Content);
                foreach (var item in data.results)
                {
                    if (list.Count < total_movie)
                        list.Add(item);
                }

                if (data.total_pages > page && list.Count < total_movie)
                    page++;
            }

            //https://api.themoviedb.org/3/trending/all/week?api_key=2f9be36ac95e2c5a4aab564003e7d17a&page=1
            return list;
        }

        public List<Movie> MappingModel(List<TheMovie> movies)
        {
            var data = new List<Movie>();

            foreach (var item in movies.Where(x => !string.IsNullOrEmpty(x.title)))
            {
                data.Add(new Movie
                {
                    AverageScore = item.vote_average,
                    Description = item.overview,
                    Name = item.title
                });
            }

            return data;
        }
    }
}
