using Microsoft.AspNetCore.Identity;

namespace ECommerce.AppAdmin.ViewModel
{
    public class RoleDataVM
    {
        public IEnumerable< IdentityRole>? identityRole { get; set; }
        public RoleFormViewModel roleFormViewModel { get; set; }

    }
}
