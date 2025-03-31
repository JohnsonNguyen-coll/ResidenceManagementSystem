using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ResidenceHub.Models;

namespace ResidenceHub.DAL
{
    public class HouseholdDAO
    {
        private readonly AsmPrnContext _context;

        public HouseholdDAO()
        {
            _context = new AsmPrnContext();
        }

        public Household GetHouseholdById(int householdId)
        {
            return _context.Households
                .Include(h => h.HeadOfHousehold)
                .Include(h => h.HouseholdMembers)
                .FirstOrDefault(h => h.HouseholdId == householdId);
        }

        public List<Household> GetAllHouseholds()
        {
            return _context.Households
                .Include(h => h.HeadOfHousehold)
                .Include(h => h.HouseholdMembers)
                .ToList();
        }

        public List<Household> GetHouseholdsByArea(string area)
        {
            // Giả định là area là một phần của địa chỉ
            return _context.Households
                .Include(h => h.HeadOfHousehold)
                .Include(h => h.HouseholdMembers)
                .Where(h => h.Address.Contains(area))
                .ToList();
        }

        public List<Household> GetHouseholdsByHeadOfHousehold(int userId)
        {
            return _context.Households
                .Include(h => h.HeadOfHousehold)
                .Include(h => h.HouseholdMembers)
                .Where(h => h.HeadOfHouseholdId == userId)
                .ToList();
        }

        public bool CreateHousehold(Household household)
        {
            try
            {
                _context.Households.Add(household);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateHousehold(Household household)
        {
            try
            {
                _context.Entry(household).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteHousehold(int householdId)
        {
            try
            {
                var household = _context.Households.Find(householdId);
                if (household != null)
                {
                    _context.Households.Remove(household);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}