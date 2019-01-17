using SEWKTand.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.MedicalRecord
{
    public class MedicalRecordDTO
    {
        public int Id { get; set; }

        public int PatientId { get; set; } //Foreign key
        public EntityPatient Patient { get; set; } //Navigation property

        public DateTime DateOfNote { get; set; }
        public string Note { get; set; }
    }

    public class CreateMedicalRecordDTO
    {
        public string Note { get; set; }
    }
}
