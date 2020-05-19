using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.DTOs
{
    public class AuthorisationDTO
    {
        public int idUser { get; set; }
        public string login { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string token { get; set; }
    }
}
