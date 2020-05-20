using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.DTOs.NewQuizDTOs;
using JavaCourseAPI.Services.QuizServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {

        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }


        [HttpPut("addQuiz")]
        public async Task<IActionResult> AddQuizAsync(NewQuizDTO newQuizDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _quizService.AddQuizAsync(newQuizDTO);
                return Ok();
            }
            catch (Helpers.AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getQuizesInCategory/{categoryId}")]
        public async Task<IActionResult> GetUserCategoriesAsync([FromRoute] int categoryId)
        {
            var quizes = await _quizService.GetQuizesInCategory(categoryId);
            return Ok(quizes);
        }
        //QuizToAnswer


        [HttpGet("getQuizToAnswer/{quizId}")]
        public async Task<IActionResult> GetQuizToAnswerAsync([FromRoute] int quizId)
        {
            var quiz = await _quizService.GetQuizToAnswerAsync(quizId);
            return Ok(quiz);
        }


        [HttpPut("quizAnswer")]
        public async Task<IActionResult> SaveAnswerForQuizAsync(AnswerQuizDTO answerQuiz)
        {
            await _quizService.SaveAnswerForQuizAsync(answerQuiz);
            return Ok();
        }

        [HttpGet("testAPI")]
        public IActionResult TestAPI()
        {
            return Ok();
        }
    }
}