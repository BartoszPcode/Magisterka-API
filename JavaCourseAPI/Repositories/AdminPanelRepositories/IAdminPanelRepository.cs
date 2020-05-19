using JavaCourseAPI.DTOs;
using JavaCourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Repositories.AdminPanelRepositories
{
    public interface IAdminPanelRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<List<StudentGroup>> GetAllGroupsAsync();
        Task<User> FindUserAsync(UsersForAdminPanel user);
        Task UpdateUserAsync(User user);
        Task AddNewGroupAsync(StudentGroup studentGroup);
    }
}
