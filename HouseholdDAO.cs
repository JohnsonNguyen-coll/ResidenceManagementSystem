using Microsoft.EntityFrameworkCore;
using ResidenceHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace ResidenceHub.DataAccess
{
    public class HouseholdDAO
    {
        private readonly AsmPrnContext _context;

        public HouseholdDAO(AsmPrnContext context)
        {
            _context = context;
        }

        public List<Household> GetAllHouseholds()
        {
            return _context.Households.ToList();
        }

        public void AddHousehold(Household household)
        {
            _context.Households.Add(household);
            _context.SaveChanges();
        }

        public void UpdateHousehold(Household household)
        {
            _context.Households.Update(household);
            _context.SaveChanges();
        }

        public void DeleteHousehold(int householdId)
        {
            var household = _context.Households.Find(householdId);
            if (household != null)
            {
                _context.Households.Remove(household);
                _context.SaveChanges();
            }
        }

        public Household CreateSeparation(Household originalHousehold, int newHeadId, string newAddress)
        {
            var newHousehold = new Household
            {
                HeadOfHouseholdId = newHeadId,
                Address = newAddress,
                CreatedDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.Households.Add(newHousehold);
            _context.SaveChanges();
            return newHousehold;
        }
    }
}