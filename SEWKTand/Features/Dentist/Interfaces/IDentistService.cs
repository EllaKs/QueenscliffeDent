using SEWKTand.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.Dentist.Interfaces
{
    public interface IDentistService
    {
        Task<EntityDentist> GetByIdAsync(int id);
        Task<IList<EntityDentist>> GetAllAsync();
        Task<EntityDentist> RegisterAsync(EntityDentist model);
        Task<EntityDentist> UpdateAsync(int id, EntityDentist model);
        Task DeleteAsync(int id);
    }
}
