using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public class UserRepo : IRepository
    {
        private static List<User> users = new List<User>()
        {
            new User(){UserID = 1,UserName = "Admin",Password = "Admin@123"},
            new User(){UserID = 2,UserName = "Wasim",Password = "123456"},
            new User(){UserID = 3,UserName = "Raza",Password = "abcdef"},
            new User(){UserID = 4,UserName = "Messi",Password = "GOAT10"}
        };
        public User Login(User user)
        {
            return users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
        }
    }
}
