using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Patient.Interfaces;
using SEWKTand.Features.Shared;

namespace SEWKTand.Features.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientServices;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientServices, IMapper mapper)
        {
            _patientServices = patientServices;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> AllAsync()
        {
            try
            {
                var patient = await _patientServices.GetAllAsync();
                var mappedPatient = _mapper.Map<IList<PatientDTO>>(patient);
                return Ok(mappedPatient);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ByIdAsync(int id)
        {
            try
            {
                var user = await _patientServices.GetByIdAsync(id);
                var mappedUser = _mapper.Map<PatientDTO>(user);
                return Ok(mappedUser);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterPatientDTO model)
        {
            if (ModelState.IsValid) //The RegisterPatientDTO Errormessage fires before
            {
                try
                {
                    await _patientServices.CheckIfEmailExistAsync(model.Email);
                }
                catch (AppException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
                 
                var mappedUser = _mapper.Map<EntityPatient>(model);
                mappedUser.SocialSecurityNumber = model.Year + model.Month + model.Day + model.LastDigits;

                try
                {
                    await _patientServices.RegisterAsync(mappedUser);
                    return Ok(model);
                }
                catch (AppException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }               
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdatePatientDTO model)
        {
            var mappedPatient = _mapper.Map<EntityPatient>(model);

            try
            {           
                var user = await _patientServices.UpdateAsync(id, mappedPatient);
                return Ok(user);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _patientServices.DeleteAsync(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[HttpPost]
        //[Route("note")]
        //public async Task<IActionResult> CreateNoteAsync(int id, CreateMedicalRecordDTO note)
        //{
        //    var mappedNote = _mapper.Map<EntityMedicalRecord>(note);
        //    try
        //    {
        //        await _patientServices.CreateNoteAsync(id, mappedNote);
        //        return Ok();
        //    }
        //    catch (AppException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpGet] // Not working...
        //[Route("note")]
        //public async Task<IActionResult> AllNotesAsync(int id)
        //{
        //    try
        //    {
        //        var patientNotes = await _patientServices.AllNotesAsync(id);
        //       // var mappedUser = _mapper.Map<PatientDTO>(user);
        //        return Ok(patientNotes);
        //    }
        //    catch (AppException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}
    }
}