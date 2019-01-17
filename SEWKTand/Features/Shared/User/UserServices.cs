using Microsoft.EntityFrameworkCore;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Shared.Security.Interfaces;
using SEWKTand.Interfaces.Data;
using System.Threading.Tasks;

namespace SEWKTand.Features.Shared.User
{
    public class UserServices : IUserServices
    {
        private readonly IDataContext _context;
        private readonly IGenerateSecurePassword _securePassword;

        public UserServices(IDataContext context, IGenerateSecurePassword securePassword)
        {
            _context = context;
            _securePassword = securePassword;
        }

        public async Task<Person> LoginAsync(string email, string password)
        {
            var admin = await _context.Admin
                       .SingleOrDefaultAsync(a => a.Email == email);

            if (admin != null)
            {
                var result = _securePassword.CheckIfPasswordIsLegit(password, admin.HashedPassword);
                if (!result)
                {
                    throw new AppException("The provided password is incorrect");
                }

                return await Task.FromResult(admin);
            }
            else
            {
                var dentist = await _context.Dentist
             .SingleOrDefaultAsync(a => a.Email == email);

                if (dentist != null)
                {
                    var result = _securePassword.CheckIfPasswordIsLegit(password, dentist.HashedPassword);
                    if (!result)
                    {
                        throw new AppException("The provided password is incorrect");
                    }

                    return await Task.FromResult(dentist);
                }
            }
            throw new AppException("The provided email doesn't exist");
        }

        public async Task CheckIfEmailExistAsync(string email)
        {
            Task<bool> t1 = _context.Admin.AnyAsync(a => a.Email == email);
            Task<bool> t2 = _context.Dentist.AnyAsync(a => a.Email == email);

            await Task.WhenAll(t1, t2);

            if (t1.Result.Equals(true) || t2.Result.Equals(true))
            {
                throw new AppException("The provided email already exist.");
            }
            return;
        }
    }
}
