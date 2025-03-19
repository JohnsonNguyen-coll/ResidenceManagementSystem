using ResidenceHub.Models;

namespace ResidenceHub.DataAccess
{
    public class ReportDAO
    {
        private readonly AsmPrnContext _context;

        public ReportDAO(AsmPrnContext context)
        {
            _context = context;
        }

        public (int householdCount, int memberCount) GetHouseholdStats()
        {
            var householdCount = _context.Households.Count();
            var memberCount = _context.HouseholdMembers.Count();
            return (householdCount, memberCount);
        }
    }
}