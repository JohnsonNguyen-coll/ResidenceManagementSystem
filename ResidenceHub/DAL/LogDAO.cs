using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ResidenceHub.Models;

namespace ResidenceHub.DAL
{
    public class LogDAO
    {
        private readonly AsmPrnContext _context;

        public LogDAO()
        {
            _context = new AsmPrnContext();
        }

        public Log GetLogById(int logId)
        {
            return _context.Logs
                .Include(l => l.User)
                .FirstOrDefault(l => l.LogId == logId);
        }

        public List<Log> GetAllLogs()
        {
            return _context.Logs
                .Include(l => l.User)
                .OrderByDescending(l => l.Timestamp)
                .ToList();
        }

        public List<Log> GetLogsByUser(int userId)
        {
            return _context.Logs
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.Timestamp)
                .ToList();
        }

        public List<Log> GetLogsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Logs
                .Include(l => l.User)
                .Where(l => l.Timestamp >= startDate && l.Timestamp <= endDate)
                .OrderByDescending(l => l.Timestamp)
                .ToList();
        }

        public List<Log> GetLogsByAction(string action)
        {
            return _context.Logs
                .Include(l => l.User)
                .Where(l => l.Action.Contains(action))
                .OrderByDescending(l => l.Timestamp)
                .ToList();
        }

        public bool CreateLog(Log log)
        {
            try
            {
                _context.Logs.Add(log);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateLog(int userId, string action)
        {
            try
            {
                var log = new Log
                {
                    UserId = userId,
                    Action = action,
                    Timestamp = DateTime.Now
                };
                _context.Logs.Add(log);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteLog(int logId)
        {
            try
            {
                var log = _context.Logs.Find(logId);
                if (log != null)
                {
                    _context.Logs.Remove(log);
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