using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviewRecommend.API.Models
{
    public class ApiResponse
    {
        public object Message { get; set; }
        public bool IsSuccess { get; set; }
        public object Object { get; set; }
    }
}
