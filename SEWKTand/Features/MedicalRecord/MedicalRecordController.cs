using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEWKTand.Data.Entities;
using SEWKTand.Features.MedicalRecord.Interfaces;
using SEWKTand.Features.Shared;
namespace SEWKTand.Features.MedicalRecord
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IMedicalRecordService _medicalRecordServices;

        public MedicalRecordController(IMapper mapper, IMedicalRecordService medicalRecordServices)
        {
            _mapper = mapper;
            _medicalRecordServices = medicalRecordServices;
        }

        [HttpPost]
        [Route("note")]
        public async Task<IActionResult> CreateNoteAsync(int id, CreateMedicalRecordDTO note)
        {
            var mappedNote = _mapper.Map<EntityMedicalRecord>(note);
            try
            {
                await _medicalRecordServices.CreateNoteAsync(id, mappedNote);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet] // Not working...
        [Route("note")]
        public async Task<IActionResult> AllNotesAsync(int id)
        {
            try
            {
                var patientNotes = await _medicalRecordServices.AllNotesAsync(id);
                // var mappedUser = _mapper.Map<PatientDTO>(user);
                return Ok(patientNotes);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}