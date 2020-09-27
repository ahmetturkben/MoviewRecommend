using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.DAL.Entities
{
    public class User : BaseDALEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
