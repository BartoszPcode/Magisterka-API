using JavaCourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Repositories.QuizRepositories
{
    public interface IQuizRepository
    {
        Task AddQuizAsync(Quiz quiz);
        Task AddQuestionSingleChoiceAsync(QuestionSingleChoice questionSC);
        Task AddPossibleAnswerSC(PossibleAnswerSc possibleAnswerSC);
        Task AddQuestionOrderingAsync(QuestionOrdering questionOrdering);
        Task AddAnswerBlock(AnswerOrdering answerOrdering);
        Task<List<Quiz>> GetQuizesInCategory(int categoryId);
        Task<Quiz> GetQuizToAnswerAsync(int quizId);
        Task<List<QuestionSingleChoice>> GetSingleChoiceQuestionsAsync(int quizId);
        Task<List<QuestionOrdering>> GetOrderingQuestionsAsync(int quizId);
        Task SaveAnswerForQuizAsync(UserQuizAnswers userAnswer);
        Task<UserQuizAnswers> CheckIfUserAnswerExistAsync(int userId, int quizId);
        Task UpdateUserAnswerAsync(UserQuizAnswers userAnswer);
    }
}
