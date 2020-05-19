using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs.ExerciseDTOs
{
    public class ExerciseAnswerDTO
    {
        public int IdUser { get; set; }
        public int IdExercise { get; set; }
        public string ExerciseUserAnswer { get; set; }
        public DateTime ExerciseAnswerDate { get; set; }
    }
}
