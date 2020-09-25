using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.DAL.DTO
{
    public class RepositoryResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }

        public RepositoryResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
