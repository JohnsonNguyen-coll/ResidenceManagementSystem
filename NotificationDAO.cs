using ResidenceHub.Models;
using System;

namespace ResidenceHub.DataAccess
{
    public class NotificationDAO
    {
        private readonly AsmPrnContext _context;

        public NotificationDAO(AsmPrnContext context)
        {
            _context = context;
        }

        public void SendNotification(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                SentDate = DateTime.Now,
                IsRead = false
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
    }
}