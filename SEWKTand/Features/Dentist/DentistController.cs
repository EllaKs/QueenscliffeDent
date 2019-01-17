using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Dentist.Interfaces;
using SEWKTand.Features.Shared;
using SEWKTand.Features.Shared.User;

namespace SEWKTand.Features.Dentist
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistController : ControllerBase
    {
        private readonly IDentistService _dentistServices;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public DentistController(IDentistService dentistServices, IMapper mapper, IUserServices userServices)
        {
            _dentistServices = dentistServices;
            _userServices = userServices;
            _mapper = mapper;
        }

        [AllowAnonymous] // Might be useless...
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> AllAsync()
        {
            try
            {
                var dentist = await _dentistServices.GetAllAsync();
                var mappedDentist = _mapper.Map<IList<UserDTO>>(dentist);
                return Ok(mappedDentist);
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
                var user = await _dentistServices.GetByIdAsync(id);
                var mappedUser = _mapper.Map<UserDTO>(user);
                return Ok(mappedUser);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDTO model)
        {
            if (ModelState.IsValid) //The RegisterUserDTO Errormessage fires before
            {
                var mappedUser = _mapper.Map<EntityDentist>(model);
                mappedUser.Email = ($"{model.FirstName.Substring(0, 1)}{model.LastName}@guldtand.com").ToLower();

                try
                {
                    await _userServices.CheckIfEmailExistAsync(mappedUser.Email);
                }
                catch (AppException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }

                try
                {
                    await _dentistServices.RegisterAsync(mappedUser);
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserDTO model)
        {
            var mappedDentist = _mapper.Map<EntityDentist>(model);
            mappedDentist.Email = ($"{model.FirstName.Substring(0, 1)}{model.LastName}@guldtand.com").ToLower();

            try
            {
                mappedDentist.HashedPassword = model.NewPassword;
                var user = await _dentistServices.UpdateAsync(id, mappedDentist);
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
                await _dentistServices.DeleteAsync(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
