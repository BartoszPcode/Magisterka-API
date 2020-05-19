using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class UserQuizAnswers
    {
        public int IdUserQuizAnswers { get; set; }
        public int IdUser { get; set; }
        public int IdQuiz { get; set; }
        public int UserScore { get; set; }
        public bool Passed { get; set; }
        public DateTime AnsweredDate { get; set; }

        public virtual Quiz IdQuizNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
