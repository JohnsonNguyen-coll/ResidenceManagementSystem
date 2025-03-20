using Microsoft.EntityFrameworkCore;
using ResidenceHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace ResidenceHub.DataAccess
{
    public class RegistrationDAO
    {
        private readonly AsmPrnContext _context;

        public RegistrationDAO(AsmPrnContext context)
        {
            _context = context;
        }

        public List<Registration> GetPendingRegistrations()
        {
            return _context.Registrations.Where(r => r.Status == "Pending").ToList();
        }

        public void ApproveRegistration(Registration registration, int policeId, string comments)
        {
            registration.Status = "Approved";
            registration.ApprovedBy = policeId;
            registration.Comments = comments;
            _context.SaveChanges();
        }

        public void RejectRegistration(Registration registration, int policeId, string comments)
        {
            registration.Status = "Rejected";
            registration.ApprovedBy = policeId;
            registration.Comments = comments;
            _context.SaveChanges();
        }

        public Dictionary<string, int> GetResidentStats()
        {
            return _context.Registrations
                .GroupBy(r => r.RegistrationType)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}