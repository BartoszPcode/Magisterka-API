using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class CodeSplitedDTO
    {
        public string classInCode { get; set; }
        public int classCyclomaticComplexity { get; set; }
        public List<string> functionsInClass { get; set; }
        public List<string> functionsInClassWithoutWhiteSpaces { get; set; }
        public List<int> functionsCyclomaticComplexity { get; set; }
        public List<string> functionsInClassToDisplay { get; set; }
    }
}
