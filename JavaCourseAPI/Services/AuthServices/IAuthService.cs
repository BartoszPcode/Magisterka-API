using JavaCourseAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services
{
    public interface IAuthService
    {
        //Task<ActionResult<string>> GetAuthAsync(LoginDTO loginDTO);
        IActionResult RegisterNewUser(RegisterDTO registerDTO);
        Task<IActionResult> AuthenticateUser(LoginDTO loginDTO);
    }
}
