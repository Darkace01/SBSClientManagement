using Microsoft.AspNetCore.Identity;
using SBSClientManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _ctx;

        public UserRepo(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IdentityUser GetUserById(string userId)
        {
            if (String.IsNullOrEmpty(userId))
                throw new ArgumentNullException("User not found");

            return _ctx.Users.Where(u => u.Id == userId).FirstOrDefault();
        }
    }
}
