using JavaCourseAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.TestCodeServices
{
    public interface ITestCodeService
    {
        string generateGraph(string classPath, string className);
        Task<TestCodeSendInfoDTO> getCodeInformations(string javaCode);
        void generateInformationFiles(string fileWithCodePath);
        Task<TestCodeAllInfoDTO> GenerateAllInfo(string javaCode);
    }
}
