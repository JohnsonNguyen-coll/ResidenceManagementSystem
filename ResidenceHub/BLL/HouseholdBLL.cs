using System;
using System.Collections.Generic;
using ResidenceHub.DAL;
using ResidenceHub.Models;

namespace ResidenceHub.BLL
{
    public class HouseholdBLL
    {
        private readonly HouseholdDAO _householdDAO;
        private readonly HouseholdMemberDAO _memberDAO;
        private readonly UserDAO _userDAO;
        private readonly LogDAO _logDAO;

        public HouseholdBLL()
        {
            _householdDAO = new HouseholdDAO();
            _memberDAO = new HouseholdMemberDAO();
            _userDAO = new UserDAO();
            _logDAO = new LogDAO();
        }

        public Household GetHouseholdById(int householdId)
        {
            return _householdDAO.GetHouseholdById(householdId);
        }

        public List<Household> GetAllHouseholds()
        {
            return _householdDAO.GetAllHouseholds();
        }

        public List<Household> GetHouseholdsByArea(string area)
        {
            return _householdDAO.GetHouseholdsByArea(area);
        }

        public List<Household> GetHouseholdsByHeadOfHousehold(int userId)
        {
            return _householdDAO.GetHouseholdsByHeadOfHousehold(userId);
        }

        public bool CreateHousehold(Household household, int createdByUserId)
        {
            if (household.HeadOfHouseholdId == null || string.IsNullOrEmpty(household.Address))
                return false;

            var headOfHousehold = _userDAO.GetUserById(household.HeadOfHouseholdId.Value);
            if (headOfHousehold == null)
                return false;

            if (household.CreatedDate == null)
            {
                household.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
            }

            bool result = _householdDAO.CreateHousehold(household);

            if (result)
            {
                var member = new HouseholdMember
                {
                    HouseholdId = household.HouseholdId,
                    UserId = household.HeadOfHouseholdId,
                    Relationship = "Head"
                };
                _memberDAO.CreateMember(member);

                _logDAO.CreateLog(createdByUserId, $"Created new household with ID {household.HouseholdId}");
            }

            return result;
        }

        public bool UpdateHousehold(Household household, int updatedByUserId)
        {
            if (household.HeadOfHouseholdId == null || string.IsNullOrEmpty(household.Address))
                return false;

            var existingHousehold = _householdDAO.GetHouseholdById(household.HouseholdId);
            if (existingHousehold == null)
                return false;

            var headOfHousehold = _userDAO.GetUserById(household.HeadOfHouseholdId.Value);
            if (headOfHousehold == null)
                return false;

            bool result = _householdDAO.UpdateHousehold(household);

            if (result)
            {
                if (existingHousehold.HeadOfHouseholdId != household.HeadOfHouseholdId)
                {
                    var oldHeadMembers = _memberDAO.GetMembersByHousehold(household.HouseholdId)
                        .FindAll(m => m.UserId == existingHousehold.HeadOfHouseholdId && m.Relationship == "Head");
                    foreach (var oldHead in oldHeadMembers)
                    {
                        oldHead.Relationship = "Member";
                        _memberDAO.UpdateMember(oldHead);
                    }

                    var newHeadMembers = _memberDAO.GetMembersByHousehold(household.HouseholdId)
                        .FindAll(m => m.UserId == household.HeadOfHouseholdId);

                    if (newHeadMembers.Count > 0)
                    {
                        foreach (var newHead in newHeadMembers)
                        {
                            newHead.Relationship = "Head";
                            _memberDAO.UpdateMember(newHead);
                        }
                    }
                    else
                    {
                        var newHeadMember = new HouseholdMember
                        {
                            HouseholdId = household.HouseholdId,
                            UserId = household.HeadOfHouseholdId,
                            Relationship = "Head"
                        };
                        _memberDAO.CreateMember(newHeadMember);
                    }
                }

                _logDAO.CreateLog(updatedByUserId, $"Updated household with ID {household.HouseholdId}");
            }

            return result;
        }

        public bool DeleteHousehold(int householdId, int deletedByUserId)
        {
            var household = _householdDAO.GetHouseholdById(householdId);
            if (household == null)
                return false;

            var members = _memberDAO.GetMembersByHousehold(householdId);
            foreach (var member in members)
            {
                _memberDAO.DeleteMember(member.MemberId);
            }

            bool result = _householdDAO.DeleteHousehold(householdId);

            if (result)
            {
                _logDAO.CreateLog(deletedByUserId, $"Deleted household with ID {householdId}");
            }

            return result;
        }

        public bool TransferHousehold(int householdId, string newAddress, int transferredByUserId)
        {
            // Check if the household exists
            var household = _householdDAO.GetHouseholdById(householdId);
            if (household == null)
                return false;

            // Update the address
            household.Address = newAddress;
            bool result = _householdDAO.UpdateHousehold(household);

            if (result)
            {
                // Log the action
                _logDAO.CreateLog(transferredByUserId, $"Transferred household with ID {householdId} to new address");
            }

            return result;
        }
    }
}