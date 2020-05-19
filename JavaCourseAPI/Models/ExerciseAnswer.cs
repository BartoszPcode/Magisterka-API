using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class ExerciseAnswer
    {
        public ExerciseAnswer()
        {
            ExerciseAnswerSimilarityIdExerciseAnswerPlagiarismNavigation = new HashSet<ExerciseAnswerSimilarity>();
            ExerciseAnswerSimilarityIdExerciseAnswerRoleModelNavigation = new HashSet<ExerciseAnswerSimilarity>();
        }

        public int IdExerciseAnswer { get; set; }
        public int IdUser { get; set; }
        public int IdExercise { get; set; }
        public string ExerciseUserAnswer { get; set; }
        public DateTime ExerciseAnswerDate { get; set; }
        public bool PlagiarismByComputer { get; set; }
        public bool PlagiarismByTeacher { get; set; }
        public bool WaitingForTeacherVerification { get; set; }
        public int IdAnswerClassStatistics { get; set; }

        public virtual AnswerClassStatistics IdAnswerClassStatisticsNavigation { get; set; }
        public virtual Exercise IdExerciseNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<ExerciseAnswerSimilarity> ExerciseAnswerSimilarityIdExerciseAnswerPlagiarismNavigation { get; set; }
        public virtual ICollection<ExerciseAnswerSimilarity> ExerciseAnswerSimilarityIdExerciseAnswerRoleModelNavigation { get; set; }
    }
}
