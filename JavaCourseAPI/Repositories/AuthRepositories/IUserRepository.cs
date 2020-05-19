using JavaCourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Repositories.AuthRepositories
{
    public interface IUserRepository
    {
        void CanICreateUser(User user, string password);
        void AddNewUser(User user);

        User GetByLogin(string login);
    }
}
