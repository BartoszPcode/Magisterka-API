using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class User
    {
        public User()
        {
            Category = new HashSet<Category>();
            ExerciseAnswer = new HashSet<ExerciseAnswer>();
            UserQuizAnswers = new HashSet<UserQuizAnswers>();
        }

        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public int? IdStudentGroup { get; set; }
        public bool Admin { get; set; }
        public bool Teacher { get; set; }
        public string AlbumNo { get; set; }
        public DateTime RegisterDate { get; set; }

        public virtual StudentGroup IdStudentGroupNavigation { get; set; }
        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<ExerciseAnswer> ExerciseAnswer { get; set; }
        public virtual ICollection<UserQuizAnswers> UserQuizAnswers { get; set; }
    }
}
