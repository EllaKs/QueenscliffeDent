using Microsoft.AspNetCore.Mvc;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEWKTand.Features.Admin.Interfaces
{
    public interface IAdminService
    {
        Task<EntityAdmin> GetByIdAsync(int id);
        Task<IList<EntityAdmin>> GetAllAsync();
        Task<EntityAdmin> RegisterAsync(EntityAdmin model);
        Task<EntityAdmin> UpdateAsync(int id, EntityAdmin model);
        Task DeleteAsync(int id);
    }
}
