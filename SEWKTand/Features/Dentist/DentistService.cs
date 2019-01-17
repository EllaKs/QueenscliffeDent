using Microsoft.EntityFrameworkCore;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Dentist.Interfaces;
using SEWKTand.Features.Shared;
using SEWKTand.Features.Shared.Security.Interfaces;
using SEWKTand.Interfaces.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.Dentist
{
    public class DentistServices : IDentistService
    {
        private readonly IDataContext _context;
        private readonly IGenerateSecurePassword _generateSecurePassword;

        public DentistServices(IDataContext context, IGenerateSecurePassword generateSecurePassword)
        {
            _context = context;
            _generateSecurePassword = generateSecurePassword;
        }

        public async Task<IList<EntityDentist>> GetAllAsync()
        {
            var listOfdentists = await _context.Dentist.ToListAsync();

            if (listOfdentists != null)
            {
                return listOfdentists;
            }
            throw new AppException("There's no existing dentists.");
        }

        public async Task<EntityDentist> GetByIdAsync(int id)
        {
            var dentist = await _context.Dentist.FindAsync(id);

            if (dentist != null)
            {
                return dentist;
            }

            throw new AppException("The provided id doesn't exist.");
        }

        public async Task<EntityDentist> UpdateAsync(int id, EntityDentist model)
        {
            var dentist = await _context.Dentist
                .SingleOrDefaultAsync(a => a.Id == id);

            if (dentist != null)
            {
                dentist.FirstName = model.FirstName;
                dentist.LastName = model.LastName;
                var hashedPassword = _generateSecurePassword.HashAndSaltPassword(model.HashedPassword);
                model.HashedPassword = hashedPassword;
                dentist.HashedPassword = model.HashedPassword;
                dentist.Email = ($"{model.FirstName.Substring(0, 1)}{model.LastName}@guldtand.com").ToLower();
                _context.Dentist.UpdateRange();
                await _context.SaveChangesAsync();

                return dentist;
            }
            throw new AppException("Unable to update. Provided id doesn't exist.");
        }

        public async Task<EntityDentist> RegisterAsync(EntityDentist model)
        {
            if (_context.Dentist.Any(x => x.Email == model.Email))
            {
                throw new AppException($"Email {model.Email} already exists.");
            }
            else
            {
                var hashedPassword = _generateSecurePassword.HashAndSaltPassword(model.HashedPassword);
                model.HashedPassword = hashedPassword;

                _context.Dentist.Add(model);
                await _context.SaveChangesAsync();

                return model;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var dentist = await _context.Dentist.FindAsync(id);

            if (dentist != null)
            {
                _context.Dentist.Remove(dentist);
                await _context.SaveChangesAsync();
                return;
            }

            throw new AppException("Unable to delete. The provided id doesn't exist.");
        }
    }
}
