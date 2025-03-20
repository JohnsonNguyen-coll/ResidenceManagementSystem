using System;
using System.Collections.Generic;

namespace ResidenceHub.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<HouseholdMember> HouseholdMembers { get; set; } = new List<HouseholdMember>();

    public virtual ICollection<Household> Households { get; set; } = new List<Household>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Registration> RegistrationApprovedByNavigations { get; set; } = new List<Registration>();

    public virtual ICollection<Registration> RegistrationUsers { get; set; } = new List<Registration>();
}
