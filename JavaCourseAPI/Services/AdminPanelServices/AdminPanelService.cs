using AutoMapper;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.Models;
using JavaCourseAPI.Repositories.AdminPanelRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.AdminPanelServices
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly IMapper _mapper;
        private readonly IAdminPanelRepository _adminPanelRepository;

        public AdminPanelService(IAdminPanelRepository adminPanelRepository, IMapper mapper)
        {
            _adminPanelRepository = adminPanelRepository;
            _mapper = mapper;
        }

        public async Task<List<UsersForAdminPanel>> GetAllUsersAsync()
        {
            var users = await _adminPanelRepository.GetAllUsersAsync();
            List<UsersForAdminPanel> usersDTO = _mapper.Map<List<UsersForAdminPanel>>(users);

            return usersDTO;
        }

        public async Task<List<StudentGroupDTO>> GetAllGroupsAsync()
        {
            var groups = await _adminPanelRepository.GetAllGroupsAsync();
            List<StudentGroupDTO> groupsDTO = _mapper.Map<List<StudentGroupDTO>>(groups);
            return groupsDTO;
        }
        
        public async Task AddNewGroupAsync(string groupName)
        {
            StudentGroup studentGroup = new StudentGroup();
            studentGroup.GroupName = groupName;

            await _adminPanelRepository.AddNewGroupAsync(studentGroup);
        }

        public async Task UpdateUserAsync(UsersForAdminPanel user) 
        {
            User foundUser = await _adminPanelRepository.FindUserAsync(user);

            if (user.StudentGroup != null)
            {
                if (user.StudentGroup.IdStudentGroup != -1)
                {
                    foundUser.IdStudentGroup = user.StudentGroup.IdStudentGroup;
                }
                else
                {
                    foundUser.IdStudentGroup = null;
                }
            }          
            else
            {
                foundUser.IdStudentGroup = null;
            }

            foundUser.Admin = user.Admin;
            foundUser.Teacher = user.Teacher;

            await _adminPanelRepository.UpdateUserAsync(foundUser);
        }
    }
}
