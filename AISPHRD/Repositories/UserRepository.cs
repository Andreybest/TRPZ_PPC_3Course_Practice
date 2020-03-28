using AISPHRD.Data;
using AISPHRD.Models;
using System.Linq;

namespace AISPHRD.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUser(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
