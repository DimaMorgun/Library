using Microsoft.AspNet.Identity.EntityFramework;

namespace Library.EntityLayer.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
