using System.Threading.Tasks;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.Services.TestCodeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JavaCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCodeController : ControllerBase
    {
        private readonly ITestCodeService _testCodeService;

        public TestCodeController(ITestCodeService testCodeService)
        {
            _testCodeService = testCodeService;
        }

        [AllowAnonymous]
        [HttpPost("getInformation")]
        public async Task<TestCodeSendInfoDTO> TestCode(TestCodeRecievedDTO testCodeRecieved)
        {
            var test = await _testCodeService.getCodeInformations(testCodeRecieved.codeJava);

            return test;
        }

    }
}
