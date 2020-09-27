using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviewRecommend.API.Models
{
    public class MovieDetails
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int Rate { get; set; }
        public decimal AveragateRate { get; set; }
    }
}
