using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public Task<IActionResult> SignInAsync(LoginDTO loginDTO)
        {
            return  _authService.AuthenticateUser(loginDTO);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterDTO registerDTO)
        {
            return _authService.RegisterNewUser(registerDTO);
        }

    }
}