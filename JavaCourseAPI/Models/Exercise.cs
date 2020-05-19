using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class Exercise
    {
        public Exercise()
        {
            ExerciseAnswer = new HashSet<ExerciseAnswer>();
        }

        public int IdExercise { get; set; }
        public int IdCategory { get; set; }
        public string ExerciseQuestion { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<ExerciseAnswer> ExerciseAnswer { get; set; }
    }
}
