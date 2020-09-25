using AutoMapper;
using MoviewRecommend.DAL.Interfaces;
using MoviewRecommend.Service.Interfaces;

namespace MoviewRecommend.Service.Services
{
    public class NoteService : ServiceBase<DAL.Entities.Note, BLL.Note>, INoteService
    {
        public NoteService(IMapper mapper, INoteRepository noteRepository) : base(mapper)
        {
            base.InitializeBase(noteRepository);
        }
    }
}
