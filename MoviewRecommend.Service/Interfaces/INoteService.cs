using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.Service.Interfaces
{
    public interface INoteService : IService<DAL.Entities.Note, BLL.Note>
    {
    }
}
