using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.DAL.Entities
{
    public class Movie : BaseDALEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AverageScore { get; set; }
    }
}
