using JavaCourseAPI.DTOs;
using JavaCourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.AdminPanelServices
{
    public interface IAdminPanelService
    {
        Task<List<UsersForAdminPanel>> GetAllUsersAsync();
        Task<List<StudentGroupDTO>> GetAllGroupsAsync();
        Task UpdateUserAsync(UsersForAdminPanel user);
        Task AddNewGroupAsync(string groupName);
    }
}
