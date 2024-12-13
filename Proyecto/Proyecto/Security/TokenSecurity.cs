using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class TokenSecurity
    {
        public string GeneratedEncryptToken(string data, string encryptionKey)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.GenerateIV();
                using (var encryptor = aes.CreateEncryptor())
                {
                    var encryptedBytes = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
                    var ivAndEncryptedData = aes.IV.Concat(encryptedBytes).ToArray();
                    return Convert.ToBase64String(ivAndEncryptedData);
                }
            }
        }

        public string DecryptToken(string encryptedToken, string encryptionKey)
        {
            var fullBytes = Convert.FromBase64String(encryptedToken);
            var keyBytes = Encoding.UTF8.GetBytes(encryptionKey);

            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;

                var iv = fullBytes.Take(16).ToArray();
                var encryptedData = fullBytes.Skip(16).ToArray();
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                {
                    var decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
