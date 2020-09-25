using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.BLL.BaseEntity
{
    public interface IEntityBase
    {
        void SetModifiedDetail();
        void SetCreatedDetail();
        void SetIsDeletedToTrue();
    }
}
