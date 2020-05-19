using JavaCourseAPI.DTOs;
using JavaCourseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Repositories.AdminPanelRepositories
{
    public class AdminPanelRepository : IAdminPanelRepository
    {
        private readonly Magisterka_v1Context _dbContext;

        public AdminPanelRepository(Magisterka_v1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var x = await _dbContext.User
                        .Include(user => user.IdStudentGroupNavigation).ToListAsync();
            return x;
        }

        public async Task<List<StudentGroup>> GetAllGroupsAsync()
        {
            return await _dbContext.StudentGroup.ToListAsync();
        }

        public async Task<User> FindUserAsync(UsersForAdminPanel user)
        {
            return await _dbContext.User.Where(userDb => userDb.IdUser == user.IdUser).FirstOrDefaultAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewGroupAsync(StudentGroup studentGroup)
        {
            await _dbContext.StudentGroup.AddAsync(studentGroup);
            await _dbContext.SaveChangesAsync();
        }
    }
}
