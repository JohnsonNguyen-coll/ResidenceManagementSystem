using System;
using System.Text.RegularExpressions;

namespace ResidenceHub.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                // Sử dụng regex đơn giản để kiểm tra email
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            // Mật khẩu phải có ít nhất 6 ký tự
            return !string.IsNullOrEmpty(password) && password.Length >= 6;
        }

        public static bool IsValidDateRange(DateOnly startDate, DateOnly? endDate)
        {
            // Nếu endDate là null thì coi như hợp lệ
            if (endDate == null)
                return true;

            // Ngày kết thúc phải sau ngày bắt đầu
            return endDate.Value > startDate;
        }

        public static bool IsValidRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                return false;

            // Kiểm tra role có hợp lệ không
            return role == "Citizen" || role == "AreaLeader" || role == "Police" || role == "Admin";
        }

        public static bool IsValidRegistrationType(string type)
        {
            if (string.IsNullOrEmpty(type))
                return false;

            // Kiểm tra loại đăng ký có hợp lệ không
            return type == "Permanent" || type == "Temporary" || type == "TemporaryStay";
        }
    }
}