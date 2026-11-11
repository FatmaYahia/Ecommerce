using System.ComponentModel.DataAnnotations;

namespace ECommerce.AppAdmin.ViewModel
{
    public class RoleFormViewModel
    {
        [Required,StringLength(256)]
        public string Name { get; set; }
    }
}
