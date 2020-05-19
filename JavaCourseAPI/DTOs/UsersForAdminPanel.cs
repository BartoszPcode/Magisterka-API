using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class UsersForAdminPanel
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public StudentGroupDTO StudentGroup { get; set; }
        public bool Admin { get; set; }
        public bool Teacher { get; set; }
        public string AlbumNo { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
