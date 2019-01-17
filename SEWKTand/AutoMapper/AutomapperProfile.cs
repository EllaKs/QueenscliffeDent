using AutoMapper;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Admin;
using SEWKTand.Features.Dentist;
using SEWKTand.Features.MedicalRecord;
using SEWKTand.Features.Patient;
using SEWKTand.Features.Shared.Interfaces;
using SEWKTand.Features.Shared.User;

namespace SEWKTand.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EntityAdmin, UserDTO>().ReverseMap();
            CreateMap<EntityDentist, UserDTO>().ReverseMap();
            CreateMap<EntityDentist, RegisterUserDTO>().ReverseMap();
            CreateMap<EntityAdmin, RegisterUserDTO>().ReverseMap();
            CreateMap<Person, UserDTO>().ReverseMap();
            CreateMap<EntityDentist, UpdateUserDTO>().ReverseMap();
            CreateMap<EntityAdmin, UpdateUserDTO>().ReverseMap();
            CreateMap<EntityPatient, PatientDTO>().ReverseMap();
            CreateMap<EntityPatient, RegisterPatientDTO>().ReverseMap();
            CreateMap<EntityPatient, UpdatePatientDTO>().ReverseMap();
            CreateMap<EntityMedicalRecord, MedicalRecordDTO>().ReverseMap();
            CreateMap<EntityMedicalRecord, CreateMedicalRecordDTO>().ReverseMap();
        }
    }
}
