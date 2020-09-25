using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.BLL
{
    public class Movie : BaseEntity.EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AverageScore { get; set; }
    }
}
