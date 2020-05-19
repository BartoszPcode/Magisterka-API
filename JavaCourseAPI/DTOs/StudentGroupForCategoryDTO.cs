using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class StudentGroupForCategoryDTO
    {
        public List<StudentGroupDTO> allGroups { get; set; }

        public List<string> signedGroupsSimple { get; set; }
        public List<string> otherGroupsSimple { get; set; }
    }
}
