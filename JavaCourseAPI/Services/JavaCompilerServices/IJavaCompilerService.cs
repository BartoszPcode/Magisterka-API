using JavaCourseAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services
{
    public interface IJavaCompilerService
    {
        Task<ActionResult<string>> JavaCompileAsync(CompileDTO compileDTO);

        Task<List<CodeAnalyzeInfoDTO>> JavaCyclomaticComplexity(CompileDTO compileDTO);
    }
}
