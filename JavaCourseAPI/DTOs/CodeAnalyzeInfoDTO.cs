using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class CodeAnalyzeInfoDTO
    {
        public string classNameToDisplay { get; set; }
        public int classCyclomaticComplexity { get; set; }
        public List<string> functionsInClassToDisplay { get; set; }
        public List<int> functionsCyclomaticComplexity { get; set; }
    }
}
