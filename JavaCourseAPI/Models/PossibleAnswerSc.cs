using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class PossibleAnswerSc
    {
        public int IdPossibleAnswerSc { get; set; }
        public string PossibleAnswer { get; set; }
        public int IdQuestionSingleChoice { get; set; }

        public virtual QuestionSingleChoice IdQuestionSingleChoiceNavigation { get; set; }
    }
}
