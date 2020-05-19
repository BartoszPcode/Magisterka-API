using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            Exercise = new HashSet<Exercise>();
            GroupToCategory = new HashSet<GroupToCategory>();
            Quiz = new HashSet<Quiz>();
        }

        public int IdCategory { get; set; }
        public string CategoryName { get; set; }
        public int IdUser { get; set; }
        public DateTime CategoryCreatedDate { get; set; }

        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<Exercise> Exercise { get; set; }
        public virtual ICollection<GroupToCategory> GroupToCategory { get; set; }
        public virtual ICollection<Quiz> Quiz { get; set; }
    }
}
