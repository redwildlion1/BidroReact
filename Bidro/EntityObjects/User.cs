using System.ComponentModel.DataAnnotations;
using Bidro.Types;

namespace Bidro.EntityObjects;

public class User(
    string email,
    string passwordHash,
    string phoneNumber,
    string firstName,
    string lastName)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(50)] public string Email { get; set; } = email;

    [StringLength(100)] public string PasswordHash { get; set; } = passwordHash;

    [StringLength(50)] public string FirstName { get; set; } = firstName;

    [StringLength(50)] public string LastName { get; set; } = lastName;

    public UserRoles Role { get; set; } = UserRoles.User;

    [StringLength(36)] public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();

    // Optional features
    public bool EmailConfirmed { get; set; } = false;

    [StringLength(13)] public string PhoneNumber { get; set; } = phoneNumber;

    public bool PhoneNumberConfirmed { get; set; } = false;
    public bool TwoFactorEnabled { get; set; } = false;
    public bool LockoutEnabled { get; set; } = false;
    public DateTimeOffset? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; } = 0;

    // Navigation properties
    public List<Firm>? Firms { get; set; }
    public List<Listing>? Listings { get; set; }
}