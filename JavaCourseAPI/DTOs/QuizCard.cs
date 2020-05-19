using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class QuizCard
    {
        public int idCategory { get; set; }
        public string quizCategoryTitle { get; set; }
        public bool allPassed { get; set; }
        public List<QuizInCardDisplayDTO> quizes { get; set; }
    }
}
