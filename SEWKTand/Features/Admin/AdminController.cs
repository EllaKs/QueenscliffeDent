using Microsoft.AspNetCore.Mvc;
using SEWKTand.Features.Admin.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEWKTand.Features.Shared;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Shared.User;
using Microsoft.AspNetCore.Authorization;

namespace SEWKTand.Features.Admin
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminServices;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminServices, IMapper mapper, IUserServices userServices)
        {
            _adminServices = adminServices;
            _userServices = userServices;
            _mapper = mapper;
        }

        [AllowAnonymous] 
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> AllAsync() // Might be useless... as we already have a controller in user who lists all users.
        {
            try
            {
                var admin = await _adminServices.GetAllAsync();
                var mappedAdmin = _mapper.Map<IList<UserDTO>>(admin);
                return Ok(mappedAdmin);
            }
            catch(AppException ex)
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
                var user = await _adminServices.GetByIdAsync(id);
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
            if (ModelState.IsValid) //The RegisterAdminDTO Errormessage fires before
            {
                var mappedUser = _mapper.Map<EntityAdmin>(model);
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
                    await _adminServices.RegisterAsync(mappedUser);
                    return Ok(mappedUser);
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserDTO model) // Change password
        {
            var mappedAdmin = _mapper.Map<EntityAdmin>(model);
            mappedAdmin.Email = ($"{model.FirstName.Substring(0, 1)}{model.LastName}@guldtand.com").ToLower();
            try
            {
                mappedAdmin.HashedPassword = model.NewPassword;
                var user = await _adminServices.UpdateAsync(id, mappedAdmin);
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
                await _adminServices.DeleteAsync(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
