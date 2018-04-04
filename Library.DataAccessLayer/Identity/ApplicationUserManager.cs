using Library.EntityLayer.Identity;
using Microsoft.AspNet.Identity;

namespace Library.DataAccessLayer.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }
    }
}
