using JavaCourseAPI.DTOs;
using JavaCourseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Repositories.CategoryRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Magisterka_v1Context _dbContext;

        public CategoryRepository(Magisterka_v1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetUserCategoriesAsync(int idUser)
        {
            var x = await _dbContext.Category
                        .Include(user => user.IdUserNavigation)
                        .Where(cat => cat.IdUserNavigation.IdUser == idUser).ToListAsync();
            return x;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _dbContext.Category.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Quiz>> GetQuizesWithCategoriesAsync(int categoryId)
        {
            return await _dbContext.Quiz
                                    .Include(category => category.IdCategoryNavigation)
                                    .Include(answers => answers.UserQuizAnswers)
                                    .Where(c => c.IdCategory == categoryId).ToListAsync();
        }

        public async Task<List<GroupToCategory>> GetSignedGroupsAsync(int categoryId)
        {
            return await _dbContext.GroupToCategory
                                    .Include(group => group.IdStudentGroupNavigation)
                                    .Where(g => g.IdCategory == categoryId).ToListAsync();
        }

        public async Task<List<StudentGroup>> GetNotSignedGroupsAsync(int categoryId)
        {
            return await _dbContext.StudentGroup
                                    .Include(group => group.GroupToCategory).ToListAsync();
        }

        public async Task<List<StudentGroup>> GetAllGroupsAsync()
        {
            return await _dbContext.StudentGroup.ToListAsync();
        }

        public async Task AddGroupToCategoryAsync(GroupToCategory groupToCategory) 
        {
            await _dbContext.GroupToCategory.AddAsync(groupToCategory);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteGroupToCategoryAsync(GroupToCategoryEditDTO groupToCategory)
        {
            var groupToCategoryFound = await _dbContext.GroupToCategory
                                        .Where( gtc => gtc.IdStudentGroup == groupToCategory.IdStudentGroup 
                                                       && gtc.IdCategory == groupToCategory.CategoryId )
                                        .FirstOrDefaultAsync();
            _dbContext.GroupToCategory.Remove(groupToCategoryFound);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserStudenGroupAsync(int userId)
        {
            return await _dbContext.User
                        .Include(user => user.IdStudentGroupNavigation)
                        .ThenInclude(groupTo => groupTo.GroupToCategory)
                        .Where(g => g.IdUser == userId).FirstOrDefaultAsync();
        }
    }
}
