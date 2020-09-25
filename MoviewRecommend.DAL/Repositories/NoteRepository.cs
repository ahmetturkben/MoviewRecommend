using MoviewRecommend.DAL.Entities;
using MoviewRecommend.DAL.Infrastructure;
using MoviewRecommend.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoviewRecommend.DAL.Repositories
{
    public class NoteRepository : RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(AppDbContext context)
        {
            base.InitializeBase(context);
        }

        public override Expression<Func<Note, bool>> SearchFilters(Note obj)
        {
            throw new NotImplementedException();
        }
    }
}
