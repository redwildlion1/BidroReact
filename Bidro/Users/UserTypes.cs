using System.ComponentModel.DataAnnotations;
using Bidro.Firms;
using Microsoft.AspNetCore.Identity;

namespace Bidro.Users;

public static class UserTypes
{
    public class UserAccount(string userName, string firstName, string lastName) : IdentityUser<Guid>(userName)
    {
        [StringLength(50)]
        public string FirstName { get; set; } = firstName;
        [StringLength(50)]
        public string LastName { get; set; } = lastName;
    }

    public class FirmAccount(Guid firmId)
    {
        [Key]
        public Guid UserAccountId { get; set; }
        public Guid FirmId { get; set; } = firmId;
        public Firm? Firm { get; set; }
        public UserAccount? UserAccount { get; set; }
    }
    
    public class AdminAccount(Guid? createdById)
    { 
        [Key]
        public Guid UserAccountId { get; set; }
        public Guid? CreatedById { get; set; } = createdById;
        public UserAccount? UserAccount { get; set; }
    };
}