using AutoMapper;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.Models;
using JavaCourseAPI.Repositories.CategoryRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoriesDisplayDTO>> GetUserCategoriesAsync(int idUser)
        {
            var categories = await _categoryRepository.GetUserCategoriesAsync(idUser);
            List<CategoriesDisplayDTO> categoriesDTO = _mapper.Map<List<CategoriesDisplayDTO>>(categories);

            return categoriesDTO;
        }

        public async Task AddCategoryAsync(CategoryAddDTO categoryAddDTO)
        {
            Category category = _mapper.Map<Category>(categoryAddDTO);
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task AddGroupToCategoryAsync(GroupToCategoryEditDTO groupToCategoryEditDTO) 
        {
            GroupToCategory groupToCategory = new GroupToCategory();
            groupToCategory.IdCategory = groupToCategoryEditDTO.CategoryId;
            groupToCategory.IdStudentGroup = groupToCategoryEditDTO.IdStudentGroup;

            await _categoryRepository.AddGroupToCategoryAsync(groupToCategory);
        }

        public async Task DeleteGroupToCategoryAsync(GroupToCategoryEditDTO groupToCategoryEditDTO)
        {
            await _categoryRepository.DeleteGroupToCategoryAsync(groupToCategoryEditDTO);
        }

        public async Task<StudentGroupForCategoryDTO> GetGroupsForCategoryAsync(int categoryId)
        {
            StudentGroupForCategoryDTO studentGroupForCategoryDTO = new StudentGroupForCategoryDTO();
            studentGroupForCategoryDTO.signedGroupsSimple = new List<string>();
            studentGroupForCategoryDTO.otherGroupsSimple = new List<string>();

            var signed = await _categoryRepository.GetSignedGroupsAsync(categoryId);
            List<StudentGroupDTO> signedDTO = _mapper.Map<List<StudentGroupDTO>>(signed);

            var otherGroups = await _categoryRepository.GetNotSignedGroupsAsync(categoryId);

            if (signedDTO.Count() > 0)
            {
                for (int i = 0; i < signedDTO.Count(); i++)
                {
                    var alreadySigned = otherGroups.Where(obj => obj.IdStudentGroup == signedDTO[i].IdStudentGroup).FirstOrDefault();
                    otherGroups.Remove(alreadySigned);
                    studentGroupForCategoryDTO.signedGroupsSimple.Add(signedDTO[i].GroupName);
                }
            }
            
            List<StudentGroupDTO> otherGroupsDTO = _mapper.Map<List<StudentGroupDTO>>(otherGroups);
            if (otherGroups.Count() > 0)
            {
                for (int i = 0; i < otherGroups.Count(); i++)
                {
                    studentGroupForCategoryDTO.otherGroupsSimple.Add(otherGroups[i].GroupName);
                }
            }

            var allGroups = await _categoryRepository.GetAllGroupsAsync();

            studentGroupForCategoryDTO.allGroups = _mapper.Map<List<StudentGroupDTO>>(allGroups); ;

            return studentGroupForCategoryDTO;
        }

        public async Task<List<QuizCard>> GetCategoriesWithQuizesAsync(int userId)
        {
            var user = await _categoryRepository.GetUserStudenGroupAsync(userId);
            List<int> userSignedCategoriesIds = new List<int>();
            var quizesInCategories = new List<Quiz>();
            List<QuizCard> quizCards = new List<QuizCard>();

            if (user.IdStudentGroupNavigation.GroupToCategory.Count > 0)
            {
                for (int i =0; i < user.IdStudentGroupNavigation.GroupToCategory.Count; i++)
                {
                    userSignedCategoriesIds.Add(user.IdStudentGroupNavigation.GroupToCategory.ToList()[i].IdCategory);
                }


                for (int i =0; i < userSignedCategoriesIds.Count; i++)
                {
                    var quizesInSelectedCategory = await _categoryRepository.GetQuizesWithCategoriesAsync(userSignedCategoriesIds[i]);
                    for (int j = 0; j < quizesInSelectedCategory.Count; j++)
                    {
                        quizesInCategories.Add(quizesInSelectedCategory[j]);
                    }
                }

                List<Quiz> distinctCategories = quizesInCategories
                                                              .GroupBy(p => p.IdCategory)
                                                              .Select(g => g.First())
                                                              .ToList();

                for (int x = 0; x < distinctCategories.Count; x++)
                {
                    QuizCard quizCard = new QuizCard();                  
                    quizCard.quizes = new List<QuizInCardDisplayDTO>();
                    quizCard.idCategory = distinctCategories[x].IdCategory;
                    quizCard.quizCategoryTitle = distinctCategories[x].IdCategoryNavigation.CategoryName;
                    quizCard.allPassed = true;

                    var quizesInThisCategory = quizesInCategories.Where(quiz => quiz.IdCategory == distinctCategories[x].IdCategory).ToList();

                    for (int i = 0; i < quizesInThisCategory.Count; i++)
                    {                  
                        QuizInCardDisplayDTO quizInCardDTO = new QuizInCardDisplayDTO();
                        quizInCardDTO.quizId = quizesInThisCategory[i].IdQuiz;
                        quizInCardDTO.quizTitle = quizesInThisCategory[i].QuizName;
                        quizInCardDTO.maxPoints = quizesInThisCategory[i].MaxPoints;
                        quizInCardDTO.pointsToPass = quizesInThisCategory[i].PointsToPass;
                        //quizInCardDTO.quizId = quizesInCategories[i].IdQuiz;
                        // quizInCardDTO.quizId = quizesInCategories[i].IdQuiz;

                        if (quizesInThisCategory[i].UserQuizAnswers.Count > 0)
                        {
                            var userAnswer = quizesInThisCategory[i].UserQuizAnswers.Where(answer => answer.IdUser == userId).FirstOrDefault();

                            if (userAnswer != null)
                            {
                                quizInCardDTO.userPoints = userAnswer.UserScore;
                                quizInCardDTO.userPassed = userAnswer.Passed;
                                quizInCardDTO.tookPart = true;
                            }
                            else
                            {
                                quizInCardDTO.userPoints = 0;
                                quizInCardDTO.userPassed = false;
                                quizInCardDTO.tookPart = false;
                                quizCard.allPassed = false;
                            }
                        }
                        else
                        {
                            quizInCardDTO.userPoints = 0;
                            quizInCardDTO.userPassed = false;
                            quizInCardDTO.tookPart = false;
                            quizCard.allPassed = false;
                        }

                        quizCard.quizes.Add(quizInCardDTO);
                    }
                    quizCards.Add(quizCard);
                }
            }
            return quizCards;
        }
    }
}
