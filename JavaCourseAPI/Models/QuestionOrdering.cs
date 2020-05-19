using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class QuestionOrdering
    {
        public QuestionOrdering()
        {
            AnswerOrdering = new HashSet<AnswerOrdering>();
        }

        public int IdQuestionOrdering { get; set; }
        public string QuestionOrdering1 { get; set; }
        public int IdQuiz { get; set; }
        public int PointsO { get; set; }

        public virtual Quiz IdQuizNavigation { get; set; }
        public virtual ICollection<AnswerOrdering> AnswerOrdering { get; set; }
    }
}
