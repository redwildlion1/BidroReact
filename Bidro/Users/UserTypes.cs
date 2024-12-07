using System.ComponentModel.DataAnnotations;
using Bidro.Firms;
using Microsoft.AspNetCore.Identity;

namespace Bidro.Users;

public class UserTypes
{
    public class UserAccount : IdentityUser<Guid>
    {
        public UserAccount(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public UserAccount(string userName, string firstName, string lastName) : base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class FirmAccount : IdentityUser<Guid>
    {
        public FirmAccount(string firmName)
        {
            FirmName = firmName;
        }

        public FirmAccount(string userName, string firmName) : base(userName)
        {
            FirmName = firmName;
        }

        [StringLength(50)]
        public string FirmName { get; set; }
        public Firm? Firm { get; set; }
    }
    
    public class AdminAccount(string userName) : IdentityUser<Guid>(userName);
}