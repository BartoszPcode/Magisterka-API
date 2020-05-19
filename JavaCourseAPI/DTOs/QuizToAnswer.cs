using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class QuizToAnswer
    {
        public int quizId { get; set; }
        public List<QuizQuestion> quizQuestions { get; set; }
        public string quizTitle { get; set; }
        public int maxPoints { get; set; }
        public int pointsToPass { get; set; }
        public int timeForQuiz { get; set; }
    }
}
