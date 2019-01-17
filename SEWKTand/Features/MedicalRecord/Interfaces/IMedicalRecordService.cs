using SEWKTand.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.MedicalRecord.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<EntityPatient> CreateNoteAsync(int id, EntityMedicalRecord model);
        Task<IList<EntityMedicalRecord>> AllNotesAsync(int id);
    }
}
