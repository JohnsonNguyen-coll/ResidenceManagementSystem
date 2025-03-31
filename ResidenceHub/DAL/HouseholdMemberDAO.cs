using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ResidenceHub.Models;

namespace ResidenceHub.DAL
{
    public class HouseholdMemberDAO
    {
        private readonly AsmPrnContext _context;

        public HouseholdMemberDAO()
        {
            _context = new AsmPrnContext();
        }

        public HouseholdMember GetMemberById(int memberId)
        {
            return _context.HouseholdMembers
                .Include(m => m.Household)
                .Include(m => m.User)
                .FirstOrDefault(m => m.MemberId == memberId);
        }

        public List<HouseholdMember> GetAllMembers()
        {
            return _context.HouseholdMembers
                .Include(m => m.Household)
                .Include(m => m.User)
                .ToList();
        }

        public List<HouseholdMember> GetMembersByHousehold(int householdId)
        {
            return _context.HouseholdMembers
                .Include(m => m.User)
                .Where(m => m.HouseholdId == householdId)
                .ToList();
        }

        public List<HouseholdMember> GetHouseholdsByMember(int userId)
        {
            return _context.HouseholdMembers
                .Include(m => m.Household)
                .Where(m => m.UserId == userId)
                .ToList();
        }

        public bool CreateMember(HouseholdMember member)
        {
            try
            {
                _context.HouseholdMembers.Add(member);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMember(HouseholdMember member)
        {
            try
            {
                _context.Entry(member).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMember(int memberId)
        {
            try
            {
                var member = _context.HouseholdMembers.Find(memberId);
                if (member != null)
                {
                    _context.HouseholdMembers.Remove(member);
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

        public bool TransferMember(int memberId, int newHouseholdId)
        {
            try
            {
                var member = _context.HouseholdMembers.Find(memberId);
                if (member != null)
                {
                    member.HouseholdId = newHouseholdId;
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