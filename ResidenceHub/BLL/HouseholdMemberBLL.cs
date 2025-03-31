using System;
using System.Collections.Generic;
using ResidenceHub.DAL;
using ResidenceHub.Models;

namespace ResidenceHub.BLL
{
    public class HouseholdMemberBLL
    {
        private readonly HouseholdMemberDAO _memberDAO;
        private readonly HouseholdDAO _householdDAO;
        private readonly UserDAO _userDAO;
        private readonly LogDAO _logDAO;

        public HouseholdMemberBLL()
        {
            _memberDAO = new HouseholdMemberDAO();
            _householdDAO = new HouseholdDAO();
            _userDAO = new UserDAO();
            _logDAO = new LogDAO();
        }

        public HouseholdMember GetMemberById(int memberId)
        {
            return _memberDAO.GetMemberById(memberId);
        }

        public List<HouseholdMember> GetAllMembers()
        {
            return _memberDAO.GetAllMembers();
        }

        public List<HouseholdMember> GetMembersByHousehold(int householdId)
        {
            return _memberDAO.GetMembersByHousehold(householdId);
        }

        public List<HouseholdMember> GetHouseholdsByMember(int userId)
        {
            return _memberDAO.GetHouseholdsByMember(userId);
        }

        public bool CreateMember(HouseholdMember member, int createdByUserId)
        {
            if (member.HouseholdId == null || member.UserId == null || string.IsNullOrEmpty(member.Relationship))
                return false;

            var household = _householdDAO.GetHouseholdById(member.HouseholdId.Value);
            if (household == null)
                return false;

            var user = _userDAO.GetUserById(member.UserId.Value);
            if (user == null)
                return false;

            var existingMembers = _memberDAO.GetMembersByHousehold(member.HouseholdId.Value);
            if (existingMembers.Exists(m => m.UserId == member.UserId))
                return false;

            bool result = _memberDAO.CreateMember(member);

            if (result)
            {
                // Log the action
                _logDAO.CreateLog(createdByUserId, $"Added user {member.UserId} to household {member.HouseholdId}");
            }

            return result;
        }

        public bool UpdateMember(HouseholdMember member, int updatedByUserId)
        {
            if (member.HouseholdId == null || member.UserId == null || string.IsNullOrEmpty(member.Relationship))
                return false;

            var existingMember = _memberDAO.GetMemberById(member.MemberId);
            if (existingMember == null)
                return false;

            var household = _householdDAO.GetHouseholdById(member.HouseholdId.Value);
            if (household == null)
                return false;

            var user = _userDAO.GetUserById(member.UserId.Value);
            if (user == null)
                return false;

            bool result = _memberDAO.UpdateMember(member);

            if (result)
            {
                _logDAO.CreateLog(updatedByUserId, $"Updated member {member.MemberId} in household {member.HouseholdId}");
            }

            return result;
        }

        public bool DeleteMember(int memberId, int deletedByUserId)
        {
            var member = _memberDAO.GetMemberById(memberId);
            if (member == null)
                return false;

            int? userId = member.UserId;
            int? householdId = member.HouseholdId;

            bool result = _memberDAO.DeleteMember(memberId);

            if (result)
            {
                _logDAO.CreateLog(deletedByUserId, $"Removed user {userId} from household {householdId}");
            }

            return result;
        }

        public bool TransferMember(int memberId, int newHouseholdId, int transferredByUserId)
        {
            var member = _memberDAO.GetMemberById(memberId);
            if (member == null)
                return false;

            var household = _householdDAO.GetHouseholdById(newHouseholdId);
            if (household == null)
                return false;

            int? oldHouseholdId = member.HouseholdId;

            bool result = _memberDAO.TransferMember(memberId, newHouseholdId);

            if (result)
            {
                // Log the action
                _logDAO.CreateLog(transferredByUserId, $"Transferred member {memberId} from household {oldHouseholdId} to household {newHouseholdId}");
            }

            return result;
        }

        public bool SeparateHousehold(int oldHouseholdId, List<int> memberIds, int newHeadOfHouseholdId, string newAddress, int separatedByUserId)
        {
            // Check if the old household exists
            var oldHousehold = _householdDAO.GetHouseholdById(oldHouseholdId);
            if (oldHousehold == null)
                return false;

            // Check if the new head of household is in the list of members to be separated
            if (!memberIds.Contains(newHeadOfHouseholdId))
                return false;

            // Create a new household
            var newHousehold = new Household
            {
                HeadOfHouseholdId = newHeadOfHouseholdId,
                Address = newAddress,
                CreatedDate = DateOnly.FromDateTime(DateTime.Now)
            };

            bool result = _householdDAO.CreateHousehold(newHousehold);

            if (result)
            {
                foreach (int memberId in memberIds)
                {
                    var member = _memberDAO.GetMemberById(memberId);
                    if (member != null && member.HouseholdId == oldHouseholdId)
                    {
                        if (member.UserId == newHeadOfHouseholdId)
                        {
                            member.Relationship = "Head";
                        }

                        member.HouseholdId = newHousehold.HouseholdId;
                        _memberDAO.UpdateMember(member);
                    }
                }

                _logDAO.CreateLog(separatedByUserId, $"Separated household {oldHouseholdId} to create new household {newHousehold.HouseholdId}");
            }

            return result;
        }
    }
}