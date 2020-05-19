using JavaCourseAPI.DTOs;
using JavaCourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Repositories.CategoryRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetUserCategoriesAsync(int idUser);
        Task AddCategoryAsync(Category category);
        Task<List<Quiz>> GetQuizesWithCategoriesAsync(int categoryId);
        Task<List<GroupToCategory>> GetSignedGroupsAsync(int categoryId);
        Task<List<StudentGroup>> GetNotSignedGroupsAsync(int categoryId);
        Task<List<StudentGroup>> GetAllGroupsAsync();
        Task AddGroupToCategoryAsync(GroupToCategory groupToCategory);
        Task DeleteGroupToCategoryAsync(GroupToCategoryEditDTO groupToCategory);
        Task<User> GetUserStudenGroupAsync(int userId);
    }
}
