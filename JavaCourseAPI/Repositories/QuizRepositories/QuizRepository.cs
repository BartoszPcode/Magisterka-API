using JavaCourseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Repositories.QuizRepositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly Magisterka_v1Context _dbContext;

        public QuizRepository(Magisterka_v1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddQuizAsync(Quiz quiz)
        {
            await _dbContext.Quiz.AddAsync(quiz);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddQuestionSingleChoiceAsync(QuestionSingleChoice questionSC)
        {
            await _dbContext.QuestionSingleChoice.AddAsync(questionSC);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddPossibleAnswerSC(PossibleAnswerSc possibleAnswerSC)
        {
            await _dbContext.PossibleAnswerSc.AddAsync(possibleAnswerSC);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddQuestionOrderingAsync(QuestionOrdering questionOrdering)
        {
            await _dbContext.QuestionOrdering.AddAsync(questionOrdering);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAnswerBlock(AnswerOrdering answerOrdering)
        {
            await _dbContext.AnswerOrdering.AddAsync(answerOrdering);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Quiz>> GetQuizesInCategory(int categoryId)
        {
            return await _dbContext.Quiz
                        .Where(quiz => quiz.IdCategory == categoryId).ToListAsync();
        }

        public async Task<Quiz> GetQuizToAnswerAsync(int quizId)
        {
            return await _dbContext.Quiz.Where(quiz => quiz.IdQuiz == quizId).FirstAsync();
        }

        public async Task<List<QuestionSingleChoice>> GetSingleChoiceQuestionsAsync(int quizId)
        {
            return await _dbContext.QuestionSingleChoice
                                   .Include(quiz => quiz.IdQuizNavigation)
                                   .Include(answers => answers.PossibleAnswerSc)
                                   .Where(quiz => quiz.IdQuiz == quizId).ToListAsync();
        }

        public async Task<List<QuestionOrdering>> GetOrderingQuestionsAsync(int quizId)
        {
            return await _dbContext.QuestionOrdering
                                   .Include(quiz => quiz.IdQuizNavigation)
                                   .Include(answers => answers.AnswerOrdering)
                                   .Where(quiz => quiz.IdQuiz == quizId).ToListAsync();
        }

        public async Task SaveAnswerForQuizAsync(UserQuizAnswers userAnswer)
        {
            await _dbContext.UserQuizAnswers.AddAsync(userAnswer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserQuizAnswers> CheckIfUserAnswerExistAsync(int userId, int quizId)
        {
            return await _dbContext.UserQuizAnswers
                         .Where(ans => ans.IdUser == userId && ans.IdQuiz == quizId).FirstOrDefaultAsync();
        }

        public async Task UpdateUserAnswerAsync(UserQuizAnswers userAnswer)
        {
            _dbContext.Entry(userAnswer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
