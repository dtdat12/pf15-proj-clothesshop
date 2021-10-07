using System;
using System.Text;

namespace DAL
{
    public class Md5Algorithms
    {
      public static string CreateMD5(string input)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();
                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(input)))
                    builder.Append(b.ToString("x2").ToLower());
                return builder.ToString();
            }
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> a323dde2b37753a0b80afd4caebd54e46a9c496f
