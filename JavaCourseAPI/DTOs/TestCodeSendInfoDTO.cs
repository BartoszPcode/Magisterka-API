using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class TestCodeSendInfoDTO
    {
        public string base64ImageRepresentation { get; set; }
        public List<CodeTestClassInfoDTO> codeTestClassInfo { get; set; }

        public TestCodeSendInfoDTO(string base64ImageRepresentation, List<CodeTestClassInfoDTO> codeTestClassInfo)
        {
            this.base64ImageRepresentation = base64ImageRepresentation;
            this.codeTestClassInfo = codeTestClassInfo;
        }
    }
}
