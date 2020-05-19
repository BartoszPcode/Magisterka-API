using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class RegisterDTO
    {
        public string login { get; set; }
        public string password { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string albumNo { get; set; }
        public string registerDate { get; set; }
    }
}
