using System;
using System.Collections.Generic;
using ResidenceHub.DAL;
using ResidenceHub.Models;

namespace ResidenceHub.BLL
{
    public class RegistrationBLL
    {
        private readonly RegistrationDAO _registrationDAO;
        private readonly UserDAO _userDAO;
        private readonly NotificationDAO _notificationDAO;
        private readonly LogDAO _logDAO;

        public RegistrationBLL()
        {
            _registrationDAO = new RegistrationDAO();
            _userDAO = new UserDAO();
            _notificationDAO = new NotificationDAO();
            _logDAO = new LogDAO();
        }

        public Registration GetRegistrationById(int registrationId)
        {
            return _registrationDAO.GetRegistrationById(registrationId);
        }

        public List<Registration> GetAllRegistrations()
        {
            return _registrationDAO.GetAllRegistrations();
        }

        public List<Registration> GetRegistrationsByUser(int userId)
        {
            return _registrationDAO.GetRegistrationsByUser(userId);
        }

        public List<Registration> GetRegistrationsByStatus(string status)
        {
            return _registrationDAO.GetRegistrationsByStatus(status);
        }

        public List<Registration> GetRegistrationsByArea(string area)
        {
            return _registrationDAO.GetRegistrationsByArea(area);
        }

        public List<Registration> GetRegistrationsByType(string type)
        {
            return _registrationDAO.GetRegistrationsByType(type);
        }

        public bool CreateRegistration(Registration registration)
        {
            // Validate required fields
            if (registration.UserId == null ||
                string.IsNullOrEmpty(registration.RegistrationType) ||
                registration.StartDate == null)
                return false;

            // Check if the user exists
            var user = _userDAO.GetUserById(registration.UserId.Value);
            if (user == null)
                return false;

            // Set default status to "Pending" if not provided
            if (string.IsNullOrEmpty(registration.Status))
            {
                registration.Status = "Pending";
            }

            // Create the registration
            bool result = _registrationDAO.CreateRegistration(registration);

            if (result)
            {
                // Log the action
                _logDAO.CreateLog(registration.UserId.Value, $"Created {registration.RegistrationType} registration request");
            }

            return result;
        }

        public bool UpdateRegistration(Registration registration)
        {
            if (registration.UserId == null ||
                string.IsNullOrEmpty(registration.RegistrationType) ||
                registration.StartDate == null)
                return false;

            var existingRegistration = _registrationDAO.GetRegistrationById(registration.RegistrationId);
            if (existingRegistration == null)
                return false;

            var user = _userDAO.GetUserById(registration.UserId.Value);
            if (user == null)
                return false;

            bool result = _registrationDAO.UpdateRegistration(registration);

            if (result)
            {
                _logDAO.CreateLog(registration.UserId.Value, $"Updated {registration.RegistrationType} registration request");
            }

            return result;
        }

        public bool ApproveRegistration(int registrationId, int approvedByUserId, string comments = null)
        {
            // Check if the registration exists
            var registration = _registrationDAO.GetRegistrationById(registrationId);
            if (registration == null)
                return false;

            // Check if the approver exists
            var approver = _userDAO.GetUserById(approvedByUserId);
            if (approver == null)
                return false;

            // Check if the approver has the right role
            if (approver.Role != "AreaLeader" && approver.Role != "Police")
                return false;

            // Approve the registration
            bool result = _registrationDAO.ApproveRegistration(registrationId, approvedByUserId, comments);

            if (result && registration.UserId != null)
            {
                // Create a notification for the user
                var notification = new Notification
                {
                    UserId = registration.UserId,
                    Message = $"Your {registration.RegistrationType} registration request has been approved.",
                    SentDate = DateTime.Now,
                    IsRead = false
                };
                _notificationDAO.CreateNotification(notification);

                // Log the action
                _logDAO.CreateLog(approvedByUserId, $"Approved {registration.RegistrationType} registration request for user {registration.UserId}");
            }

            return result;
        }

        public bool RejectRegistration(int registrationId, int rejectedByUserId, string comments = null)
        {
            // Check if the registration exists
            var registration = _registrationDAO.GetRegistrationById(registrationId);
            if (registration == null)
                return false;

            // Check if the rejector exists
            var rejector = _userDAO.GetUserById(rejectedByUserId);
            if (rejector == null)
                return false;

            // Check if the rejector has the right role
            if (rejector.Role != "AreaLeader" && rejector.Role != "Police")
                return false;

            // Reject the registration
            bool result = _registrationDAO.RejectRegistration(registrationId, rejectedByUserId, comments);

            if (result && registration.UserId != null)
            {
                // Create a notification for the user
                var notification = new Notification
                {
                    UserId = registration.UserId,
                    Message = $"Your {registration.RegistrationType} registration request has been rejected. " +
                              (string.IsNullOrEmpty(comments) ? "" : $"Reason: {comments}"),
                    SentDate = DateTime.Now,
                    IsRead = false
                };
                _notificationDAO.CreateNotification(notification);

                // Log the action
                _logDAO.CreateLog(rejectedByUserId, $"Rejected {registration.RegistrationType} registration request for user {registration.UserId}");
            }

            return result;
        }

        public bool DeleteRegistration(int registrationId, int deletedByUserId)
        {
            // Check if the registration exists
            var registration = _registrationDAO.GetRegistrationById(registrationId);
            if (registration == null)
                return false;

            // Store values for logging
            int? userId = registration.UserId;
            string registrationType = registration.RegistrationType;

            // Delete the registration
            bool result = _registrationDAO.DeleteRegistration(registrationId);

            if (result)
            {
                // Log the action
                _logDAO.CreateLog(deletedByUserId, $"Deleted {registrationType} registration request for user {userId}");
            }

            return result;
        }

        public List<object> GetRegistrationStatistics()
        {
            var allRegistrations = _registrationDAO.GetAllRegistrations();
            var statistics = new List<object>();

            // Group by type
            var typeGroups = allRegistrations.GroupBy(r => r.RegistrationType);
            foreach (var group in typeGroups)
            {
                statistics.Add(new
                {
                    Type = group.Key,
                    Total = group.Count(),
                    Pending = group.Count(r => r.Status == "Pending"),
                    Approved = group.Count(r => r.Status == "Approved"),
                    Rejected = group.Count(r => r.Status == "Rejected")
                });
            }

            return statistics;
        }
        public List<Registration> GetHighPriorityRequests()
        {
            // This would ideally query for registrations with a priority field set to "High"
            // For this example, we'll consider temporary residence requests as high priority
            return _registrationDAO.GetRegistrationsByType("Temporary Residence")
                                   .Where(r => r.Status == "Pending")
                                   .ToList();
        }
    }
}