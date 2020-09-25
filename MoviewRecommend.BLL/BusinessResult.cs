using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.BLL
{
    public class BusinessResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }

        public BusinessResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }

        public BusinessResult(DAL.DTO.RepositoryResult e)
        {
            Result = e.Result;
            Message = e.Message;
        }
    }
}
