using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class CompileDTO
    {
        public string code { get; set; }
        public List<string> parameters { get; set; }

    }
}
