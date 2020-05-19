using JavaCourseAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.GroovyCompilerServices
{
    public interface IGroovyCompilerService
    {
        Task<ActionResult<string>> GroovyCompileAsync(CompileDTO compileDTO);
    }
}
