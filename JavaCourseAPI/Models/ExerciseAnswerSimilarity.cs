using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class ExerciseAnswerSimilarity
    {
        public int IdSimilarity { get; set; }
        public int IdExerciseAnswerPlagiarism { get; set; }
        public int IdExerciseAnswerRoleModel { get; set; }

        public virtual ExerciseAnswer IdExerciseAnswerPlagiarismNavigation { get; set; }
        public virtual ExerciseAnswer IdExerciseAnswerRoleModelNavigation { get; set; }
    }
}
