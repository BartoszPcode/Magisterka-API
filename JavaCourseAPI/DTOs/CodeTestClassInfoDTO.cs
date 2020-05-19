using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class CodeTestClassInfoDTO
    {
        public List<SingleInformationDTO> classInformations { get; set; }
        public List<CodeTestMethodeInfoDTO> methodesInformations { get; set; }

        public CodeTestClassInfoDTO()
        {
            this.classInformations = new List<SingleInformationDTO>();
            this.methodesInformations = new List<CodeTestMethodeInfoDTO>();

        }
    }
}
