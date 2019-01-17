using Microsoft.EntityFrameworkCore;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Admin.Interfaces;
using SEWKTand.Features.Shared;
using SEWKTand.Features.Shared.Security.Interfaces;
using SEWKTand.Interfaces.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IDataContext _context;
        private readonly IGenerateSecurePassword _generateSecurePassword;

        public AdminService(IDataContext context, IGenerateSecurePassword generateSecurePassword)
        {
            _context = context;
            _generateSecurePassword = generateSecurePassword;
        }

        public async Task<IList<EntityAdmin>> GetAllAsync()
        {
            var listOfAdmins = await _context.Admin.ToListAsync();

            if (listOfAdmins != null)
            {
                return listOfAdmins;
            }
            throw new AppException("There's no existing admins.");
        }

        public async Task<EntityAdmin> GetByIdAsync(int id)
        {
            var admin = await _context.Admin.FindAsync(id);

            if (admin != null)
            {
                return admin;
            }

            throw new AppException("The provided id doesn't exist.");
        }

        public async Task<EntityAdmin> UpdateAsync(int id, EntityAdmin model)
        {
            var admin = await _context.Admin
                .SingleOrDefaultAsync(a => a.Id == id);

            if (admin != null)
            {
                admin.FirstName = model.FirstName;
                admin.LastName = model.LastName;
                admin.PhoneNumber = model.PhoneNumber;
                var hashedPassword = _generateSecurePassword.HashAndSaltPassword(model.HashedPassword);
                admin.HashedPassword = hashedPassword;
                admin.Email = ($"{model.FirstName.Substring(0, 1)}{model.LastName}@guldtand.com").ToLower(); //model.Email;
                _context.Admin.UpdateRange();
                await _context.SaveChangesAsync();

                return admin;
            }
            throw new AppException("Unable to update. Provided id doesn't exist.");
        }

        public async Task<EntityAdmin> RegisterAsync(EntityAdmin model)
        {
            if (_context.Admin.Any(x => x.Email == model.Email))
            {
                throw new AppException($"Email {model.Email} already exists.");
            }

            var hashedPassword = _generateSecurePassword.HashAndSaltPassword(model.HashedPassword);

            model.HashedPassword = hashedPassword;
            _context.Admin.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var admin = await _context.Admin.FindAsync(id);

            if (admin != null)
            {
                _context.Admin.Remove(admin);
                await _context.SaveChangesAsync();
                return;
            }

            throw new AppException("Unable to delete. The provided id doesn't exist.");
        }
    }
}