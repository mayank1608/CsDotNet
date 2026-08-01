using System.Security.Cryptography;
using System;
using System.Text;
namespace EcommWebAPI.Helpers
{
    public class PasswordHashHandler
    {

        private const int SaltSize = 16; // 128-bit salt
        private const int KeySize = 32; // 256-bit hash
        private const int Iterations = 10000; // Number of PBKDF2 iterations

        // Function to hash the password with a randomly generated salt
        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt); // Generate cryptographically secure random salt

            // Hash the password with the salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(KeySize);

                // Combine salt and hash into one string (for storage)
                byte[] hashBytes = new byte[SaltSize + KeySize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

                return Convert.ToBase64String(hashBytes); // Return the combined Base64 string
            }
        }

        // Function to validate the password by comparing it with the stored hash
        public static bool VerifyPassword(string password, string storedHash)
        {
            // Decode the Base64 string into bytes
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Extract the salt from the stored hash
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Hash the provided password with the extracted salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(KeySize);

                // Compare the newly generated hash with the stored hash
                for (int i = 0; i < KeySize; i++)
                {
                    if (hashBytes[SaltSize + i] != hash[i])
                        return false; // Hashes don't match
                }
                return true; // Hashes match
            }
        }
    }
}

