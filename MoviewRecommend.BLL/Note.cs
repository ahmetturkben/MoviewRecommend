using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.BLL
{
    public class Note : BaseEntity.EntityBase
    {
        public string Notes { get; set; }
        public int Rate { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
