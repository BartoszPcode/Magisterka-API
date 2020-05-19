using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class AnswerQuizDTO
    {
        public int idUser { get; set; }
        public int idQuiz { get; set; }
        public int userScore { get; set;  }
        public bool passed { get; set; }
        public DateTime answeredDate { get; set; }   
    }
}
