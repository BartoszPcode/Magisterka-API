using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.Services.AdminPanelServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPanelController : ControllerBase
    {
        private readonly IAdminPanelService _adminPanelService;

        public AdminPanelController(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }


        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminPanelService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("getAllGroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _adminPanelService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpPost("updateUser")]
        public async Task<IActionResult> GetAllGroups(UsersForAdminPanel user)
        {
            await _adminPanelService.UpdateUserAsync(user);
            return Ok();
        }


        [HttpPut("addGroup/{groupName}")]
        public async Task<IActionResult> AddGroup([FromRoute]string groupName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _adminPanelService.AddNewGroupAsync(groupName);
                return Ok();
            }
            catch (Helpers.AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}