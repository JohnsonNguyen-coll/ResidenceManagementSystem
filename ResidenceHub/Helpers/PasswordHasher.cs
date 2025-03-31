﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace ResidenceHub.Helpers
{
    public static class PasswordHasher
    {
        // Trong thực tế nên sử dụng BCrypt hoặc PBKDF2, nhưng đây là ví dụ đơn giản
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }
}