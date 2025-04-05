using System.ComponentModel.DataAnnotations;

namespace Bidro.Types;

public enum UserRoles
{
    [Display(Name = "User")] User,

    [Display(Name = "Firm")] Firm,

    [Display(Name = "Admin")] Admin
}