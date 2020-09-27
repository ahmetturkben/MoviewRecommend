using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.BLL.TheMovieDb
{
    public class TheMovieDbModel
    {
        public int page { get; set; }
        public List<TheMovie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class TheMovie
    {
        public string title { get; set; }
        public string overview { get; set; }
        public decimal vote_average { get; set; }
    }
}
