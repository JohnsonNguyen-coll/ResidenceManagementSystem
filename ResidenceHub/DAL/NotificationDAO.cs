using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ResidenceHub.Models;

namespace ResidenceHub.DAL
{
    public class NotificationDAO
    {
        private readonly AsmPrnContext _context;

        public NotificationDAO()
        {
            _context = new AsmPrnContext();
        }

        public Notification GetNotificationById(int notificationId)
        {
            return _context.Notifications
                .Include(n => n.User)
                .FirstOrDefault(n => n.NotificationId == notificationId);
        }

        public List<Notification> GetAllNotifications()
        {
            return _context.Notifications
                .Include(n => n.User)
                .ToList();
        }

        public List<Notification> GetNotificationsByUser(int userId)
        {
            return _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.SentDate)
                .ToList();
        }

        public List<Notification> GetUnreadNotificationsByUser(int userId)
        {
            return _context.Notifications
                .Where(n => n.UserId == userId && n.IsRead == false)
                .OrderByDescending(n => n.SentDate)
                .ToList();
        }

        public bool CreateNotification(Notification notification)
        {
            try
            {
                _context.Notifications.Add(notification);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool MarkNotificationAsRead(int notificationId)
        {
            try
            {
                var notification = _context.Notifications.Find(notificationId);
                if (notification != null)
                {
                    notification.IsRead = true;
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

        public bool MarkAllNotificationsAsRead(int userId)
        {
            try
            {
                var notifications = _context.Notifications.Where(n => n.UserId == userId && n.IsRead == false);
                foreach (var notification in notifications)
                {
                    notification.IsRead = true;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteNotification(int notificationId)
        {
            try
            {
                var notification = _context.Notifications.Find(notificationId);
                if (notification != null)
                {
                    _context.Notifications.Remove(notification);
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