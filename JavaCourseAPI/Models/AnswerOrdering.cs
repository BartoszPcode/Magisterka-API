using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class AnswerOrdering
    {
        public int IdAnswerOrdering { get; set; }
        public string AnswerBlock { get; set; }
        public int AnswerPosition { get; set; }
        public int IdQuestionOrdering { get; set; }

        public virtual QuestionOrdering IdQuestionOrderingNavigation { get; set; }
    }
}
