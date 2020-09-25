using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.BLL.BaseEntity
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public void SetModifiedDetail()
        {
            //dateModified = DateTime.Now;
        }

        public void SetCreatedDetail()
        {
            //dateCreated = DateTime.Now;
            //dateModified = DateTime.Now;
        }

        public void SetIsDeletedToTrue()
        {
            //SetModifiedDetail();
            //Active = false;
        }
    }


}
