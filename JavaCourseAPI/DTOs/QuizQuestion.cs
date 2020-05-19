using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class QuizQuestion
    {
        public string question { get; set; }
        public List<string> possibleAnswers { get; set; }
        public string correctAnswer { get; set; }
        public List<string> correctOrder { get; set; }
        public int pointsForQuestion { get; set; }
        public bool isAnswered { get; set; }
        public bool isAnsweredCorrectly { get; set; }
        public string questionType { get; set; }
        public string userAnswer { get; set; }
    }
}
