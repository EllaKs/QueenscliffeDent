using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEWKTand.Features.Admin.Interfaces;
using SEWKTand.Features.Dentist.Interfaces;
using SEWKTand.Features.Shared;

namespace SEWKTand.Features.Shared.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IAdminService _adminServices;
        private readonly IDentistService _dentistServices;
        private readonly IMapper _mapper;

        public UserController(IUserServices userServices, IMapper mapper, IAdminService adminServices, IDentistService dentistServices)
        {
            _userServices = userServices;
            _adminServices = adminServices;
            _dentistServices = dentistServices;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO model)
        {
            if (ModelState.IsValid) // Useless... The UserLoginDTO ErrormMessage fires before
            {
                string email = model.Email;
                string password = model.Password;

                try
                {
                    await _userServices.LoginAsync(email, password);

                    return Ok(model);
                }
                catch (AppException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> AllAsync()
        {
            try
            {
                var admins = await _adminServices.GetAllAsync();
                var mappedAdmins = _mapper.Map<IList<UserDTO>>(admins);
                var dentists = await _dentistServices.GetAllAsync();
                var mappedDentists = _mapper.Map<IList<UserDTO>>(dentists);

                IList<UserDTO> listOfUsers = mappedAdmins;

                foreach (var d in mappedDentists)
                {
                    listOfUsers.Add(d);
                }

                return Ok(listOfUsers);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}