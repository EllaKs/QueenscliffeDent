using Microsoft.EntityFrameworkCore;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Patient.Interfaces;
using SEWKTand.Features.Shared;
using SEWKTand.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEWKTand.Features.Shared.SSNRValidator.Interfaces;

namespace SEWKTand.Features.Patient
{
    public class PatientService : IPatientService
    {
        private readonly IDataContext _context;
        private readonly ISocialSecurityNumberVerification _ssnVerification;

        public PatientService(IDataContext context, ISocialSecurityNumberVerification ssnVerification)
        {
            _context = context;
            _ssnVerification = ssnVerification;
        }

        public async Task<IList<EntityPatient>> GetAllAsync()
        {
            //var listOfPatients = await _context.Patient.ToListAsync();
            var listOfPatients = await _context.Patient.Include(x => x.MedicalRecords).ToListAsync();

            if (listOfPatients != null)
            {
                return listOfPatients;
            }
            throw new AppException("There's no existing patients.");
        }

        public async Task<EntityPatient> GetByIdAsync(int id)
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient != null)
            {
                return patient;
            }

            throw new AppException("The provided id doesn't exist.");
        }

        public async Task CheckIfEmailExistAsync(string email)
        {
            var result = await _context.Patient.AnyAsync(a => a.Email == email);

            if (result.Equals(true))
            {
                throw new AppException("The provided email already exist.");
            }
            return;
        }

        public async Task<EntityPatient> RegisterAsync(EntityPatient model)
        {
            if (_context.Patient.Any(x => x.Email == model.Email))
            {
                throw new AppException($"Email {model.Email} already exists.");
            }

            if (!_ssnVerification.VerifyIfSocialSecurityNumberIsValid(model.SocialSecurityNumber))
            {
                throw new AppException("The provided social security number is incorrect.");
            }

            _context.Patient.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<EntityPatient> UpdateAsync(int id, EntityPatient model)
        {
            //var user = await _context.Patient.FindAsync(id);
            var patient = await _context.Patient
                .SingleOrDefaultAsync(a => a.Id == id);

            if (patient != null)
            {
                patient.Email = model.Email;
                patient.FirstName = model.FirstName;
                patient.LastName = model.LastName;
                patient.PhoneNumber = model.PhoneNumber;
                patient.Insurance = model.Insurance;
                _context.Patient.UpdateRange();
                await _context.SaveChangesAsync();

                return patient;
            }
            throw new AppException("Unable to update. Provided id doesn't exist.");
        }

        public async Task DeleteAsync(int id) //Delete all notes first!
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient != null)
            {
                _context.Patient.Remove(patient);
                await _context.SaveChangesAsync();
                return;
            }

            throw new AppException("Unable to delete. The provided id doesn't exist.");
        }

        //public async Task<EntityPatient> CreateNoteAsync(int id, EntityMedicalRecord model)
        //{
        //    var patient = await _context.Patient.FindAsync(id);

        //    if (patient != null)
        //    {
        //        var record = new EntityMedicalRecord
        //        {
        //            DateOfNote = DateTime.Now,
        //            PatientId = patient.Id,
        //            Patient = patient,
        //            Note = model.Note
        //        };

        //        _context.MedicalRecord.Add(record);
        //        await _context.SaveChangesAsync();
        //        return patient;
        //    }

        //    throw new AppException("The provided id doesn't exist.");
        //}

        //public async Task<IList<EntityMedicalRecord>> AllNotesAsync(int id)
        //{
        //    var patient = await _context.Patient
        //                        .Where(a => a.Id == id)
        //                        .Include(x => x.MedicalRecords).ToListAsync();
        //   // var patient = await _context.Patient.FindAsync(id);

        //    if (patient != null)
        //    {
        //        //var medicalrecord = patient.MedicalRecords.ToList();
        //        //if(medicalrecord == null)
        //        //{
        //        //    throw new AppException("There is no existing notes in the database.");
        //        //}
        //        //return medicalrecord;
        //    }

        //    throw new AppException("The provided id doesn't exist.");
        //}
    }
}
