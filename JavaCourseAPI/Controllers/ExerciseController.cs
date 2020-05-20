using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaCourseAPI.DTOs.ExerciseDTOs;
using JavaCourseAPI.Services.ExerciseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }


        [HttpGet("getExercisesInCategory/{categoryId}")]
        public async Task<IActionResult> GetUserCategoriesAsync([FromRoute] int categoryId)
        {
            var exercises = await _exerciseService.GetExercisesInCategoryAsync(categoryId);
            return Ok(exercises);
        }


        [HttpGet("getExercisesUserView/{categoryId}/{userId}")]
        public async Task<IActionResult> GetExercisesUserView([FromRoute] int categoryId, int userId)
        {
            var categories = await _exerciseService.GetExercisesUserViewAsync(categoryId, userId);
            return Ok(categories);
        }


        [HttpGet("getExerciseForStudent/{exerciseId}")]
        public async Task<IActionResult> GetExerciseForStudent([FromRoute] int exerciseId)
        {
            var exercise = await _exerciseService.GetExerciseForStudentAsync(exerciseId);
            return Ok(exercise);
        }


        [HttpGet("getExerciseAnswers/{exerciseId}")]
        public async Task<IActionResult> GetExerciseAnswers([FromRoute] int exerciseId)
        {
            var answers = await _exerciseService.GetExerciseAnswersAsync(exerciseId);
            return Ok(answers);
        }



        [HttpPut("addExercise")]
        public async Task<IActionResult> AddExercise(AddExerciseDTO addExerciseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _exerciseService.AddExerciseAsync(addExerciseDTO);
                return Ok();
            }
            catch (Helpers.AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("answer/pass")]
        public async Task<IActionResult> ExerciseAnswerPass(ExerciseAnswerPass exerciseAnswerPass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _exerciseService.ExerciseAnswerPassAsync(exerciseAnswerPass);
                return Ok();
            }
            catch (Helpers.AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("saveExerciseAnswer")]
        public async Task<IActionResult> SaveExerciseAnswer(ExerciseAnswerDTO exerciseAnswerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _exerciseService.SaveExerciseAnswerAsync(exerciseAnswerDTO);
                return Ok();
            }
            catch (Helpers.AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}