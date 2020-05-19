using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs.NewQuizDTOs
{
    public class QuestionSingleChoiceDTO
    {
        public string question { get; set; }
        public string correctAnswer { get; set; }
        public int pointsForQuestion { get; set; }
        public List<string> possibleAnswers { get; set; }
    }
}
