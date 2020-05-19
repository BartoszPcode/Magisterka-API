using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs.ExerciseDTOs
{
    public class PlagiarismComparisonElementDTO
    {
        public int IdExerciseAnswer { get; set; }
        public string AlbumNo { get; set; }
        public string ExerciseUserAnswer { get; set; }
    }
}
