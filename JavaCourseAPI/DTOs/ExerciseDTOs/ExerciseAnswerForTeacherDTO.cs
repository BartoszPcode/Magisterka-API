using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs.ExerciseDTOs
{
    public class ExerciseAnswerForTeacherDTO
    {
        public int Number { get; set; }
        public string AlbumNo { get; set; }
        public string GroupName { get; set; }
        public int IdExerciseAnswer { get; set; }  
        public string ExerciseUserAnswer { get; set; }
        public DateTime ExerciseAnswerDate { get; set; }
        public bool PlagiarismByComputer { get; set; }
        public bool PlagiarismByTeacher { get; set; }
        public bool WaitingForTeacherVerification { get; set; }
        public List<PlagiarismComparisonElementDTO> PlagiarismElements { get; set; }
    }
}
