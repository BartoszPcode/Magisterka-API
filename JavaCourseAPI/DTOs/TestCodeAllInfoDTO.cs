using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class TestCodeAllInfoDTO
    {
        public List<string[]> classesInfo { get; set; }
        public List<string[]> methodesInfo { get; set; }    

        public TestCodeAllInfoDTO(List<string[]> classesInfo, 
                                List<string[]> methodesInfo)
        {
            this.classesInfo = classesInfo;
            this.methodesInfo = methodesInfo;
        }
    }
}
