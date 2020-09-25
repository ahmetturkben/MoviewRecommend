

namespace MoviewRecommend.Service.Interfaces
{
    public interface IUserService : IService<DAL.Entities.User, BLL.User>
    {
        (string username, string token)? Authenticate(string username, string password);
    }
}
