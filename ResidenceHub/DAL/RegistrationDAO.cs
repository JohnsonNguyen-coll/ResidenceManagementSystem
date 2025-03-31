using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ResidenceHub.Models;

namespace ResidenceHub.DAL
{
    public class RegistrationDAO
    {
        private readonly AsmPrnContext _context;

        public RegistrationDAO()
        {
            _context = new AsmPrnContext();
        }

        public Registration GetRegistrationById(int registrationId)
        {
            return _context.Registrations
                .Include(r => r.User)
                .Include(r => r.ApprovedByNavigation)
                .FirstOrDefault(r => r.RegistrationId == registrationId);
        }

        public List<Registration> GetAllRegistrations()
        {
            return _context.Registrations
                .Include(r => r.User)
                .Include(r => r.ApprovedByNavigation)
                .ToList();
        }

        public List<Registration> GetRegistrationsByUser(int userId)
        {
            return _context.Registrations
                .Include(r => r.ApprovedByNavigation)
                .Where(r => r.UserId == userId)
                .ToList();
        }

        public List<Registration> GetRegistrationsByStatus(string status)
        {
            return _context.Registrations
                .Include(r => r.User)
                .Include(r => r.ApprovedByNavigation)
                .Where(r => r.Status == status)
                .ToList();
        }

        public List<Registration> GetRegistrationsByArea(string area)
        {
            // Giả định là area là một phần của địa chỉ và có thể truy cập thông qua User
            return _context.Registrations
                .Include(r => r.User)
                .Include(r => r.ApprovedByNavigation)
                .Where(r => r.User.Address.Contains(area))
                .ToList();
        }

        public List<Registration> GetRegistrationsByType(string type)
        {
            return _context.Registrations
                .Include(r => r.User)
                .Include(r => r.ApprovedByNavigation)
                .Where(r => r.RegistrationType == type)
                .ToList();
        }

        public bool CreateRegistration(Registration registration)
        {
            try
            {
                _context.Registrations.Add(registration);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateRegistration(Registration registration)
        {
            try
            {
                _context.Entry(registration).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ApproveRegistration(int registrationId, int approvedBy, string comments = null)
        {
            try
            {
                var registration = _context.Registrations.Find(registrationId);
                if (registration != null)
                {
                    registration.Status = "Approved";
                    registration.ApprovedBy = approvedBy;
                    if (!string.IsNullOrEmpty(comments))
                    {
                        registration.Comments = comments;
                    }
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

        public bool RejectRegistration(int registrationId, int approvedBy, string comments = null)
        {
            try
            {
                var registration = _context.Registrations.Find(registrationId);
                if (registration != null)
                {
                    registration.Status = "Rejected";
                    registration.ApprovedBy = approvedBy;
                    if (!string.IsNullOrEmpty(comments))
                    {
                        registration.Comments = comments;
                    }
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

        public bool DeleteRegistration(int registrationId)
        {
            try
            {
                var registration = _context.Registrations.Find(registrationId);
                if (registration != null)
                {
                    _context.Registrations.Remove(registration);
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