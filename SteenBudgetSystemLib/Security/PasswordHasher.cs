﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    public static byte[] GenerateSalt(int size)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var salt = new byte[size];
            rng.GetBytes(salt);
            return salt;
        }
    }

    public static string HashPasswordWithSalt(string password, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            // Combine the password and the salt
            var saltedPassword = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();

            // Compute the hash
            var hash = sha256.ComputeHash(saltedPassword);

            // Combine the hash and the salt
            var hashBytes = new byte[hash.Length + salt.Length];
            Array.Copy(hash, 0, hashBytes, 0, hash.Length);
            Array.Copy(salt, 0, hashBytes, hash.Length, salt.Length);

            // Convert to base64 for storage
            return Convert.ToBase64String(hashBytes);
        }
    }
}
