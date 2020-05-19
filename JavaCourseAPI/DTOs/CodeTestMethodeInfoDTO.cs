using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class CodeTestMethodeInfoDTO
    {
        public List<SingleInformationDTO> methodeInformations { get; set; }

        public CodeTestMethodeInfoDTO()
        {
            this.methodeInformations = new List<SingleInformationDTO>();
        }
    }
}
