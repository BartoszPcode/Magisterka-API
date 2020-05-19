using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class CodeInfoFromCSVFilesDTO
    { 
        public List<string[]> methodesInfo { get; set; }
        public List<string[]> classesInfo { get; set; }
    }
}
