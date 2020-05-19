using JavaCourseAPI.DTOs;
using JavaCourseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.CategoryServices
{
    public interface ICategoryService 
    {
        Task<List<CategoriesDisplayDTO>> GetUserCategoriesAsync(int idUser);
        Task AddCategoryAsync(CategoryAddDTO categoryAddDTO);
        Task<List<QuizCard>> GetCategoriesWithQuizesAsync(int userId);
        Task<StudentGroupForCategoryDTO> GetGroupsForCategoryAsync(int categoryId);
        Task AddGroupToCategoryAsync(GroupToCategoryEditDTO groupToCategoryEditDTO);
        Task DeleteGroupToCategoryAsync(GroupToCategoryEditDTO groupToCategoryEditDTO);
    }
}
