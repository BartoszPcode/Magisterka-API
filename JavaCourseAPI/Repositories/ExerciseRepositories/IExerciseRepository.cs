using JavaCourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Repositories.ExerciseRepositories
{
    public interface IExerciseRepository
    {
        Task<List<Exercise>> GetExercisesInCategoryAsync(int categoryId);
        Task AddExerciseAsync(Exercise exercise);
        Task<ExerciseAnswer> GetUserAnswerForExerciseAsync(int exerciseId, int userId);
        Task<Exercise> GetExerciseForStudentAsync(int exerciseId);
        Task SaveStatistics(AnswerClassStatistics stats);
        Task SaveAnswerAsync(ExerciseAnswer exerciseAnswer);
        Task<List<ExerciseAnswer>> GetAllStatsAsync(int exerciseId);
        Task<List<ExerciseAnswer>> GetExerciseAnswersAsync(int exerciseId);
        Task SaveExerciseSimilarity(ExerciseAnswerSimilarity exerciseSimilarity);
        Task<List<ExerciseAnswerSimilarity>> GetExerciseAnswerSimilarityAsync(int exerciseAnswerId);
        Task<ExerciseAnswer> GetExerciseAnswerAsync(int exerciseAnswerId);
        Task UpdateExerciseAnswerAsync(ExerciseAnswer exerciseAnswer);
        
    }
}
