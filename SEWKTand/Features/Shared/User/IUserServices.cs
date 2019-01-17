using Microsoft.AspNetCore.Mvc;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.Shared.User
{
    public interface IUserServices
    {
        Task<Person>LoginAsync(string email, string password);
        Task CheckIfEmailExistAsync(string email);
    }
}
