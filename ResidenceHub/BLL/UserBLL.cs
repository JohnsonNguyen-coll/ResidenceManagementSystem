using System;
using System.Collections.Generic;
using ResidenceHub.DAL;
using ResidenceHub.Models;

namespace ResidenceHub.BLL
{
    public class UserBLL
    {
        private readonly UserDAO _userDAO;
        private readonly LogDAO _logDAO;

        public UserBLL()
        {
            _userDAO = new UserDAO();
            _logDAO = new LogDAO();
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _userDAO.GetUserByEmail(email);

            if (user == null)
                return null;

            if (user.Password != password)
                return null;

            _logDAO.CreateLog(user.UserId, "User logged in");

            return user;
        }

        public User GetUserById(int userId)
        {
            return _userDAO.GetUserById(userId);
        }

        public User GetUserByEmail(string email)
        {
            return _userDAO.GetUserByEmail(email);
        }

        public List<User> GetAllUsers()
        {
            return _userDAO.GetAllUsers();
        }

        public List<User> GetUsersByRole(string role)
        {
            return _userDAO.GetUsersByRole(role);
        }

        public List<User> GetUsersByArea(string area)
        {
            return _userDAO.GetUsersByArea(area);
        }

        public bool CreateUser(User user)
        {
            var existingUser = _userDAO.GetUserByEmail(user.Email);
            if (existingUser != null)
                return false;

            if (string.IsNullOrEmpty(user.FullName) ||
                string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.Password) ||
                string.IsNullOrEmpty(user.Role) ||
                string.IsNullOrEmpty(user.Address))
                return false;


            return _userDAO.CreateUser(user);
        }

        public bool UpdateUser(User user)
        {
            // Validate required fields
            if (string.IsNullOrEmpty(user.FullName) ||
                string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.Password) ||
                string.IsNullOrEmpty(user.Role) ||
                string.IsNullOrEmpty(user.Address))
                return false;

            // Validate email uniqueness for other users
            var existingUser = _userDAO.GetUserByEmail(user.Email);
            if (existingUser != null && existingUser.UserId != user.UserId)
                return false;

            return _userDAO.UpdateUser(user);
        }

        public bool UpdateUserProfile(User user)
        {
            // Chỉ cho phép cập nhật thông tin cá nhân, không được thay đổi role
            var existingUser = _userDAO.GetUserById(user.UserId);
            if (existingUser == null)
                return false;

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;

            // Nếu mật khẩu được cung cấp, cập nhật mật khẩu
            if (!string.IsNullOrEmpty(user.Password))
            {
                // Trong thực tế, cần mã hóa password trước khi lưu
                existingUser.Password = user.Password;
            }

            return _userDAO.UpdateUser(existingUser);
        }

        public bool DeleteUser(int userId)
        {
            return _userDAO.DeleteUser(userId);
        }
    }
}