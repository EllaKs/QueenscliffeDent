using SEWKTand.Features.Shared.Security.Interfaces;
using System;
using System.Security.Cryptography;

namespace SEWKTand.Features.Shared.Security
{
    //public interface IRfc2898DeriveBytes
    //{
    //    Rfc2898DeriveBytes Rfc2898DeriveBytes(string password, byte[] salt, int iterations);
    //    byte[] GetBytes(int cb);
    //}

    //public class Rfc2898DeriveBytesAdapter : IRfc2898DeriveBytes
    //{
    //    private readonly Rfc2898DeriveBytes _rfcAdaptee;

    //    public Rfc2898DeriveBytesAdapter(string password, byte[] salt, int iterations)
    //    {
    //        _rfcAdaptee = new Rfc2898DeriveBytes(password, salt, iterations);
    //    }

    //    public Rfc2898DeriveBytes Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
    //    {
    //        return /*new*/ Rfc2898DeriveBytes(password, salt, iterations);
    //    }

    //    public byte[] GetBytes(int cb)
    //    {
    //       return _rfcAdaptee.GetBytes(cb);
    //    }
    //}

    public class GenerateSecurePassword : IGenerateSecurePassword
    {
        //private readonly IRfc2898DeriveBytes _rfc2898DeriveBytes;

        //public GenerateSecurePassword(IRfc2898DeriveBytes rfc2898DeriveBytes)
        //{
        //    _rfc2898DeriveBytes = rfc2898DeriveBytes;
        //}

        public string HashAndSaltPassword(string password)
        {
            var salt = GenerateSalt();
            var hash = HashPassword(password, salt);

            byte[] hashbytes = new byte[36];
            Array.Copy(salt, 0, hashbytes, 0, 16);
            Array.Copy(hash, 0, hashbytes, 16, 20);

            return Convert.ToBase64String(hashbytes);
        }

        public byte[] GenerateSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return salt;
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            //var pbkdf2 = _rfc2898DeriveBytes.Rfc2898DeriveBytes(password, salt, 10000);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            return hash;
        }

        public bool CheckIfPasswordIsLegit(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }
    }
}

