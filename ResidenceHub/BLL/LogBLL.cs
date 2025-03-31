using System;
using System.Collections.Generic;
using ResidenceHub.DAL;
using ResidenceHub.Models;

namespace ResidenceHub.BLL
{
    public class LogBLL
    {
        private readonly LogDAO _logDAO;
        private readonly UserDAO _userDAO;

        public LogBLL()
        {
            _logDAO = new LogDAO();
            _userDAO = new UserDAO();
        }

        public Log GetLogById(int logId)
        {
            return _logDAO.GetLogById(logId);
        }

        public List<Log> GetAllLogs()
        {
            return _logDAO.GetAllLogs();
        }

        public List<Log> GetLogsByUser(int userId)
        {
            return _logDAO.GetLogsByUser(userId);
        }

        public List<Log> GetLogsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _logDAO.GetLogsByDateRange(startDate, endDate);
        }

        public List<Log> GetLogsByAction(string action)
        {
            return _logDAO.GetLogsByAction(action);
        }

        public bool CreateLog(Log log)
        {
            // Validate required fields
            if (log.UserId == null || string.IsNullOrEmpty(log.Action))
                return false;

            // Check if the user exists
            var user = _userDAO.GetUserById(log.UserId.Value);
            if (user == null)
                return false;

            // Set timestamp if not provided
            if (log.Timestamp == null)
            {
                log.Timestamp = DateTime.Now;
            }

            return _logDAO.CreateLog(log);
        }

        public bool CreateLog(int userId, string action)
        {
            // Check if the user exists
            var user = _userDAO.GetUserById(userId);
            if (user == null)
                return false;

            return _logDAO.CreateLog(userId, action);
        }

        public bool DeleteLog(int logId)
        {
            return _logDAO.DeleteLog(logId);
        }

        public List<object> GetLogStatistics()
        {
            var allLogs = _logDAO.GetAllLogs();
            var statistics = new List<object>();

            // Group by date
            var dateGroups = allLogs.GroupBy(l => l.Timestamp?.Date);
            foreach (var group in dateGroups.OrderByDescending(g => g.Key))
            {
                statistics.Add(new
                {
                    Date = group.Key,
                    Count = group.Count()
                });
            }

            return statistics;
        }

        public List<object> GetUserActivityStatistics()
        {
            var allLogs = _logDAO.GetAllLogs();
            var statistics = new List<object>();

            // Group by user
            var userGroups = allLogs.GroupBy(l => l.UserId);
            foreach (var group in userGroups)
            {
                var user = _userDAO.GetUserById(group.Key.Value);
                if (user != null)
                {
                    statistics.Add(new
                    {
                        UserId = user.UserId,
                        FullName = user.FullName,
                        Email = user.Email,
                        Role = user.Role,
                        ActivityCount = group.Count(),
                        LastActivity = group.Max(l => l.Timestamp)
                    });
                }
            }

            return statistics;
        }
    }
}