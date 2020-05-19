using JavaCourseAPI.DTOs;
using JavaCourseAPI.DTOs.NewQuizDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.QuizServices
{
    public interface IQuizService
    {
        Task AddQuizAsync(NewQuizDTO newQuizDTO);
        Task<List<QuizInCategoryTableDTO>> GetQuizesInCategory(int categoryId);
        Task<QuizToAnswer> GetQuizToAnswerAsync(int quizId);
        Task SaveAnswerForQuizAsync(AnswerQuizDTO answerQuiz);
    }
}
