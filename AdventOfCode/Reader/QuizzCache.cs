using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Reader
{
    internal record CacheKey(int Year, int Day, string Token);

    internal class QuizzCache
    {
        string GetLocation(string key) => $"quiz_input_cache/{key}.txt";
        string? cachedInput = null;

        public bool TryGet(CacheKey key, out string value)
        {
            if (cachedInput != null)
            {
                value = cachedInput;
                return true;
            }

            try
            {
                var md5key = ComputeMD5Hash(key);
                var location = GetLocation(md5key);
                value = File.ReadAllText(location);
                cachedInput = value;
                return true;
            } catch
            {
                value = null;
                return false;
            }
        }

        public void Set(CacheKey key, string value) 
        {
            var md5key = ComputeMD5Hash(key);
            var location = GetLocation(md5key);
            new FileInfo(location).Directory?.Create();
            File.WriteAllText(location, value);
            cachedInput = value;
        }

        static string ComputeMD5Hash(CacheKey cacheKey)
        {
            var key = $"{cacheKey.Year}{cacheKey.Day}{cacheKey.Token}";
            using MD5 md5 = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] inputBytes = Encoding.UTF8.GetBytes(key);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to a hexadecimal string representation.
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
