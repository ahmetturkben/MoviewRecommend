using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.Service.Interfaces
{
    public interface IEmailService
    {
        bool SendMail(string email = "");
    }
}
