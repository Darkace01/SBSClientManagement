using Microsoft.AspNetCore.Identity;

namespace SBSClientManagement.Repository
{
    public interface IUserRepo
    {
        IdentityUser GetUserById(string userId);
    }
}