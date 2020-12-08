using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Data;

namespace AuthService.Repositories
{
    public class UserRepo : IRepository
    {
        private readonly AuthDbContext _context;
        public UserRepo(AuthDbContext context)
        {
            _context = context;
        }
        public User Login(User user)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
        }
    }
}
