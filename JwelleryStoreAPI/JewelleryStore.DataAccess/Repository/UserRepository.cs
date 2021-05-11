using JewelleryStore.DataAccess.Domain.Context;
using JewelleryStore.DataAccess.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(JewelleryStoreDBContext context) : base(context)
        {
        }

        public User Login(User user)
        {
            var userData = AppDataContext.Users.Include(e=>e.UserRole).Include(t=>t.UserRole.Role).FirstOrDefault(e=>e.EmailId.Equals(user.EmailId, StringComparison.OrdinalIgnoreCase) &&
                                                  e.Password.Equals(user.Password));
            return userData;
        }

        public IEnumerable<User> GetAllUser()
        {
            var users = this.GetAll();
            return users;
        }

    }
}
