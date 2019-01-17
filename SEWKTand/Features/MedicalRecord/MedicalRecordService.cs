using Microsoft.EntityFrameworkCore;
using SEWKTand.Data.Entities;
using SEWKTand.Features.MedicalRecord.Interfaces;
using SEWKTand.Features.Shared;
using SEWKTand.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.MedicalRecord
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IDataContext _context;

        public MedicalRecordService(IDataContext context)
        {
            _context = context;
        }

        public async Task<EntityPatient> CreateNoteAsync(int id, EntityMedicalRecord model)
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient != null)
            {
                var record = new EntityMedicalRecord
                {
                    DateOfNote = DateTime.Now,
                    PatientId = patient.Id,
                    Patient = patient,
                    Note = model.Note
                };

                _context.MedicalRecord.Add(record);
                await _context.SaveChangesAsync();
                return patient;
            }

            throw new AppException("The provided id doesn't exist.");
        }

        public async Task<IList<EntityMedicalRecord>> AllNotesAsync(int id)
        {
            var patient = await _context.Patient
                                .Where(a => a.Id == id)
                                .Include(x => x.MedicalRecords).ToListAsync();
            // var patient = await _context.Patient.FindAsync(id);

            if (patient != null)
            {
                //var medicalrecord = patient.MedicalRecords.ToList();
                //if(medicalrecord == null)
                //{
                //    throw new AppException("There is no existing notes in the database.");
                //}
                //return medicalrecord;
            }

            throw new AppException("The provided id doesn't exist.");
        }
    }
}
