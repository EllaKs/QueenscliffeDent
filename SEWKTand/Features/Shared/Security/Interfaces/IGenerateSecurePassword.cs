using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.Shared.Security.Interfaces
{
    public interface IGenerateSecurePassword
    {
        string HashAndSaltPassword(string password);
        bool CheckIfPasswordIsLegit(string password, string hashedPassword);
    }
}
