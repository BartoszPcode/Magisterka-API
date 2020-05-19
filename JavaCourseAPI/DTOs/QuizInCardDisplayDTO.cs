using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class QuizInCardDisplayDTO
    {
        public int quizId { get; set; }
        public string quizTitle { get; set; }
        public int maxPoints { get; set; }
        public int userPoints { get; set; }
        public int pointsToPass { get; set; }
        public bool userPassed { get; set; }
        public bool tookPart { get; set; }
    }
}
