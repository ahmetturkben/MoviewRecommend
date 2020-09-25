using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.BLL
{
    public class User : BaseEntity.EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
