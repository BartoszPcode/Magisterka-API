using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs.NewQuizDTOs
{
    public class QuestionBlocksOrdering
    {
        public string question { get; set; }
        public int pointsForQuestion { get; set; }
        public List<QuestionBlockAnswer> answers { get; set; }
    }
}
