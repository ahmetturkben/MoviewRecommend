using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.DAL.Entities
{
    public class Note : BaseDALEntity
    {
        public string Notes { get; set; }
        public int Rate { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
