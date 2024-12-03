using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace ASI.Basecode.WebApp.Models
{
    public static class EncryptionUtility
    {
        // Pad "123123" to 16 characters for AES-128
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("123123".PadRight(16, '0'));
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("Initialization12"); // 16 characters for IV

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(plainText);
                    }
                    byte[] encryptedBytes = ms.ToArray();
                    // Convert byte array to a string of digits
                    return string.Concat(encryptedBytes.Select(b => b.ToString("D3"))); // D3 formats numbers with leading zeroes
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");

            // Convert the string of digits back to byte array
            try
            {
                // Break the input string into chunks of 3 digits each, as each byte will be represented by 3 digits
                byte[] cipherBytes = Enumerable.Range(0, cipherText.Length / 3)
                    .Select(i => Convert.ToByte(cipherText.Substring(i * 3, 3)))
                    .ToArray();

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream(cipherBytes))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (FormatException)
            {
                throw new ArgumentException("Input is not a valid numerical string.");
            }
        }
    }
}
