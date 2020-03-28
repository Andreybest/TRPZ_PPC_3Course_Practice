using AISPHRD.Models;

namespace AISPHRD.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string login);

        void Update(User user);
    }
}
