using SEWKTand.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.Patient.Interfaces
{
    public interface IPatientService
    {
        Task CheckIfEmailExistAsync(string email);
        Task<EntityPatient> GetByIdAsync(int id);
        Task<IList<EntityPatient>> GetAllAsync();
        Task<EntityPatient> RegisterAsync(EntityPatient model);
        Task<EntityPatient> UpdateAsync(int id, EntityPatient model);
        Task DeleteAsync(int id);
        //Task<EntityPatient> CreateNoteAsync(int id, EntityMedicalRecord note);
        //Task<IList<EntityMedicalRecord>> AllNotesAsync(int id);
    }
}
