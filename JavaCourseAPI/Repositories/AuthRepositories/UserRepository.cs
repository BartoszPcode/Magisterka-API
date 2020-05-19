using JavaCourseAPI.Helpers;
using JavaCourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Repositories.AuthRepositories
{
    public class UserRepository: IUserRepository
    {
        private readonly Magisterka_v1Context _dbContext;

        public UserRepository(Magisterka_v1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void CanICreateUser(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_dbContext.User.Any(x => x.Login == user.Login))
                throw new AppException("Login \"" + user.Login + "\" is already taken");
        }

        public void AddNewUser(User user)
        {
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
        }

        public User GetByLogin(string login)
        {
            return _dbContext.User
                .Where(user => user.Login == login)
                .FirstOrDefault();
        }
    }
}
