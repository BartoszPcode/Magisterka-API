using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class SingleInformationDTO
    {
        public string parameterName { get; set; }
        public string parameterValue { get; set; }

        public SingleInformationDTO(string parameterName, string parameterValue)
        {
            this.parameterName = parameterName;
            this.parameterValue = parameterValue;
        }
    }
}
