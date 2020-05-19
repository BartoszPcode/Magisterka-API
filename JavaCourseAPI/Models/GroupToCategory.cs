using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class GroupToCategory
    {
        public int IdGroupToCategory { get; set; }
        public int IdStudentGroup { get; set; }
        public int IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual StudentGroup IdStudentGroupNavigation { get; set; }
    }
}
