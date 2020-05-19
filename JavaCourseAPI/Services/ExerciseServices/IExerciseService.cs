using JavaCourseAPI.DTOs.ExerciseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.ExerciseServices
{
    public interface IExerciseService
    {
        Task<List<ExerciseSimpleViewDTO>> GetExercisesInCategoryAsync(int categoryId);
        Task AddExerciseAsync(AddExerciseDTO addExerciseDTO);
        Task<List<ExerciseStudentSimpleViewDTO>> GetExercisesUserViewAsync(int categoryId, int userId);
        Task<ExerciseForStudentDTO> GetExerciseForStudentAsync(int exerciseId);
        Task SaveExerciseAnswerAsync(ExerciseAnswerDTO exerciseAnswerDTO);
        Task<List<ExerciseAnswerForTeacherDTO>> GetExerciseAnswersAsync(int exerciseId);
        Task ExerciseAnswerPassAsync(ExerciseAnswerPass exerciseAnswerPass);
    }
}
