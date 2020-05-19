using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class CategoriesDisplayDTO
    {
        public int idCategory { get; set; }
        public string categoryName { get; set; }
        public DateTime createdDate { get; set; }
    }
}
