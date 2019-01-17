using SEWKTand.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.Patient.Interfaces
{
    public interface IPatient
    {
        string SocialSecurityNumber { get; set; }
        Insurance Insurance { get; set; }
        ICollection<EntityMedicalRecord> MedicalRecords { get; set; }
    }
}