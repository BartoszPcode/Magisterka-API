using JavaCourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JavaCourseAPI.Repositories.ExerciseRepositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly Magisterka_v1Context _dbContext;

        public ExerciseRepository(Magisterka_v1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Exercise>> GetExercisesInCategoryAsync(int categoryId)
        {
            return await _dbContext.Exercise
                        .Where(exe => exe.IdCategory == categoryId).ToListAsync();
        }

        public async Task AddExerciseAsync(Exercise exercise)
        {
            await _dbContext.Exercise.AddAsync(exercise);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveStatistics(AnswerClassStatistics stats)
        {
            await _dbContext.AnswerClassStatistics.AddAsync(stats);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAnswerAsync(ExerciseAnswer exerciseAnswer)
        {
            await _dbContext.ExerciseAnswer.AddAsync(exerciseAnswer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveExerciseSimilarity(ExerciseAnswerSimilarity exerciseSimilarity)
        {
            await _dbContext.ExerciseAnswerSimilarity.AddAsync(exerciseSimilarity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ExerciseAnswer> GetUserAnswerForExerciseAsync(int exerciseId, int userId)
        {
            return await _dbContext.ExerciseAnswer
                        .Where(exe => exe.IdUser == userId && exe.IdExercise == exerciseId).FirstOrDefaultAsync();
        }

        public async Task<Exercise> GetExerciseForStudentAsync(int exerciseId)
        {
            return await _dbContext.Exercise
                       .Where(exe => exe.IdExercise == exerciseId).FirstOrDefaultAsync();
        }

        public async Task<List<ExerciseAnswer>> GetAllStatsAsync(int exerciseId) 
        {
            return await _dbContext.ExerciseAnswer
                         .Include(ans=> ans.IdAnswerClassStatisticsNavigation)
                         .Where(ans => ans.IdExercise == exerciseId).ToListAsync();
        }

        public async Task<List<ExerciseAnswer>> GetExerciseAnswersAsync(int exerciseId)
        {
            return await _dbContext.ExerciseAnswer
                         .Include(user => user.IdUserNavigation)
                         .ThenInclude(user => user.IdStudentGroupNavigation)
                         .Include(exerc => exerc.IdExerciseNavigation)
                         .Where(ans => ans.IdExercise == exerciseId).ToListAsync();
        }

        public async Task<List<ExerciseAnswerSimilarity>> GetExerciseAnswerSimilarityAsync(int exerciseAnswerId)
        {
            return await _dbContext.ExerciseAnswerSimilarity
                         .Include(answ => answ.IdExerciseAnswerRoleModelNavigation)
                         .ThenInclude(user => user.IdUserNavigation)
                         .Where(ans => ans.IdExerciseAnswerPlagiarism == exerciseAnswerId).ToListAsync();
        }

        public async Task<ExerciseAnswer> GetExerciseAnswerAsync(int exerciseAnswerId)
        {
            return await _dbContext.ExerciseAnswer
                         .Where(ans => ans.IdExerciseAnswer == exerciseAnswerId).FirstOrDefaultAsync();
        }

        public async Task UpdateExerciseAnswerAsync(ExerciseAnswer exerciseAnswer)
        {
            _dbContext.Entry(exerciseAnswer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
