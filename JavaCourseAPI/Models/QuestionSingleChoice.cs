using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class QuestionSingleChoice
    {
        public QuestionSingleChoice()
        {
            PossibleAnswerSc = new HashSet<PossibleAnswerSc>();
        }

        public int IdQuestionSingleChoice { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public int IdQuiz { get; set; }
        public int PointsSc { get; set; }

        public virtual Quiz IdQuizNavigation { get; set; }
        public virtual ICollection<PossibleAnswerSc> PossibleAnswerSc { get; set; }
    }
}
