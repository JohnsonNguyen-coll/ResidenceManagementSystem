using System;
using System.Collections.Generic;
using ResidenceHub.DAL;
using ResidenceHub.Models;

namespace ResidenceHub.BLL
{
    public class NotificationBLL
    {
        private readonly NotificationDAO _notificationDAO;
        private readonly UserDAO _userDAO;
        private readonly LogDAO _logDAO;

        public NotificationBLL()
        {
            _notificationDAO = new NotificationDAO();
            _userDAO = new UserDAO();
            _logDAO = new LogDAO();
        }

        public Notification GetNotificationById(int notificationId)
        {
            return _notificationDAO.GetNotificationById(notificationId);
        }

        public List<Notification> GetAllNotifications()
        {
            return _notificationDAO.GetAllNotifications();
        }

        public List<Notification> GetNotificationsByUser(int userId)
        {
            return _notificationDAO.GetNotificationsByUser(userId);
        }

        public List<Notification> GetUnreadNotificationsByUser(int userId)
        {
            return _notificationDAO.GetUnreadNotificationsByUser(userId);
        }

        public bool CreateNotification(Notification notification)
        {
            // Validate required fields
            if (notification.UserId == null || string.IsNullOrEmpty(notification.Message))
                return false;

            // Check if the user exists
            var user = _userDAO.GetUserById(notification.UserId.Value);
            if (user == null)
                return false;

            // Set default values if not provided
            if (notification.SentDate == null)
            {
                notification.SentDate = DateTime.Now;
            }

            if (notification.IsRead == null)
            {
                notification.IsRead = false;
            }

            return _notificationDAO.CreateNotification(notification);
        }

        public bool SendNotificationToUser(int userId, string message, int sentByUserId)
        {
            // Check if the user exists
            var user = _userDAO.GetUserById(userId);
            if (user == null)
                return false;

            // Create the notification
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                SentDate = DateTime.Now,
                IsRead = false
            };

            bool result = _notificationDAO.CreateNotification(notification);

            if (result)
            {
                // Log the action
                _logDAO.CreateLog(sentByUserId, $"Sent notification to user {userId}");
            }

            return result;
        }

        public bool SendNotificationToArea(string area, string message, int sentByUserId)
        {
            // Get all users in the area
            var users = _userDAO.GetUsersByArea(area);
            if (users.Count == 0)
                return false;

            bool allSuccessful = true;

            // Send notification to each user
            foreach (var user in users)
            {
                var notification = new Notification
                {
                    UserId = user.UserId,
                    Message = message,
                    SentDate = DateTime.Now,
                    IsRead = false
                };

                bool result = _notificationDAO.CreateNotification(notification);
                if (!result)
                {
                    allSuccessful = false;
                }
            }

            // Log the action
            _logDAO.CreateLog(sentByUserId, $"Sent notification to all users in area {area}");

            return allSuccessful;
        }

        public bool SendNotificationToRole(string role, string message, int sentByUserId)
        {
            // Get all users with the role
            var users = _userDAO.GetUsersByRole(role);
            if (users.Count == 0)
                return false;

            bool allSuccessful = true;

            // Send notification to each user
            foreach (var user in users)
            {
                var notification = new Notification
                {
                    UserId = user.UserId,
                    Message = message,
                    SentDate = DateTime.Now,
                    IsRead = false
                };

                bool result = _notificationDAO.CreateNotification(notification);
                if (!result)
                {
                    allSuccessful = false;
                }
            }

            _logDAO.CreateLog(sentByUserId, $"Sent notification to all {role} users");

            return allSuccessful;
        }

        public bool MarkNotificationAsRead(int notificationId, int userId)
        {
            var notification = _notificationDAO.GetNotificationById(notificationId);
            if (notification == null || notification.UserId != userId)
                return false;

            return _notificationDAO.MarkNotificationAsRead(notificationId);
        }

        public bool MarkAllNotificationsAsRead(int userId)
        {
            return _notificationDAO.MarkAllNotificationsAsRead(userId);
        }

        public bool DeleteNotification(int notificationId)
        {
            return _notificationDAO.DeleteNotification(notificationId);
        }
    }
}