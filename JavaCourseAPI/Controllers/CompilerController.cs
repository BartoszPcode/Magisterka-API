using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.Services;
using JavaCourseAPI.Services.GroovyCompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompilerController : ControllerBase
    {

        private readonly IJavaCompilerService _javaCompilerService;
        private readonly IGroovyCompilerService _groovyCompilerService;

        public CompilerController(IJavaCompilerService javaCompilerService, IGroovyCompilerService groovyCompilerService)
        {
            _javaCompilerService = javaCompilerService;
            _groovyCompilerService = groovyCompilerService;
        }

        [AllowAnonymous]
        [HttpPost("java/Compile")]
        public async Task<ActionResult> JavaCompile(CompileDTO compileDTO)
        {
            if ( !ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var ret = await _javaCompilerService.JavaCompileAsync(compileDTO);
                return Ok(ret);
            }          
        }

        [AllowAnonymous]
        [HttpPost("groovy/Compile")]
        public async Task<ActionResult> GroovyCompile(CompileDTO compileDTO)
        {
            var ret = await _groovyCompilerService.GroovyCompileAsync(compileDTO);
            return Ok(ret);
        }

        //JavaCyclomaticComplexityCounter
        [AllowAnonymous]
        [HttpPost("java/CyclomaticComplexity")]
        public  Task<List<CodeAnalyzeInfoDTO>> JavaCyclomaticComplexityCounter(CompileDTO compileDTO)
        {
            var ret =   _javaCompilerService.JavaCyclomaticComplexity(compileDTO);
            return ret;
        }
    }
}