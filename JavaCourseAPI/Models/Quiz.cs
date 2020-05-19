using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            QuestionOrdering = new HashSet<QuestionOrdering>();
            QuestionSingleChoice = new HashSet<QuestionSingleChoice>();
            UserQuizAnswers = new HashSet<UserQuizAnswers>();
        }

        public int IdQuiz { get; set; }
        public string QuizName { get; set; }
        public int PointsToPass { get; set; }
        public int IdCategory { get; set; }
        public DateTime QuizCreatedDate { get; set; }
        public int MaxPoints { get; set; }
        public int TimeForQuiz { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<QuestionOrdering> QuestionOrdering { get; set; }
        public virtual ICollection<QuestionSingleChoice> QuestionSingleChoice { get; set; }
        public virtual ICollection<UserQuizAnswers> UserQuizAnswers { get; set; }
    }
}
