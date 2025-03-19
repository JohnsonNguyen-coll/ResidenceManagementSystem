using System;
using System.Collections.Generic;

namespace ResidenceHub.Models;

public partial class HouseholdMember
{
    public int MemberId { get; set; }

    public int? HouseholdId { get; set; }

    public int? UserId { get; set; }

    public string Relationship { get; set; } = null!;

    public virtual Household? Household { get; set; }

    public virtual User? User { get; set; }
}
