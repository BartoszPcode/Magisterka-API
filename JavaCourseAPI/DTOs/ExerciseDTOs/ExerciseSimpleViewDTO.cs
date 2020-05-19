using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs.ExerciseDTOs
{
    public class ExerciseSimpleViewDTO
    {
        public int IdExercise { get; set; }
        public int IdCategory { get; set; }
        public string ExerciseQuestion { get; set; }
        public int exerciseNumber { get; set; }
        public bool exerciseClick { get; set; }
    }
}
