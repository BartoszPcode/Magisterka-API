using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class StudentGroup
    {
        public StudentGroup()
        {
            GroupToCategory = new HashSet<GroupToCategory>();
            User = new HashSet<User>();
        }

        public int IdStudentGroup { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<GroupToCategory> GroupToCategory { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
