using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs.NewQuizDTOs
{
    public class NewQuizDTO
    {
        public int categoryId { get; set; }
        public int userId { get; set; }
        public string quizName { get; set; }
        public int pointsToPass { get; set; }
        public DateTime quizCreatedDate { get; set; }
        public int maxPoints { get; set; }
        public int timeForQuiz { get; set; }

        public List<QuestionSingleChoiceDTO> questionsSingleChoice { get; set; }
        public List<QuestionBlocksOrdering> questionBlockOrdering { get; set; }
    }
}
