using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getUserCategories/{userId}")]
        public async Task<IActionResult> GetUserCategoriesAsync([FromRoute] int userId)
        {
            var categories = await _categoryService.GetUserCategoriesAsync(userId);
            return Ok(categories);
        }


        [HttpGet("getGroupsForCategory/{categoryId}")]
        public async Task<IActionResult> GetGroupsForCategory([FromRoute] int categoryId)
        {
            var groups = await _categoryService.GetGroupsForCategoryAsync(categoryId);
            return Ok(groups);
        }


        [HttpPut("addGroupToCategory")]
        public async Task<IActionResult> AddGroupToCategoryAsync(GroupToCategoryEditDTO groupToCategoryEditDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.AddGroupToCategoryAsync(groupToCategoryEditDTO);
                return Ok();
            }
            catch (Helpers.AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("deleteGroupToCategory")]
        public async Task<IActionResult> DeleteGroupToCategoryAsync(GroupToCategoryEditDTO groupToCategoryEditDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.DeleteGroupToCategoryAsync(groupToCategoryEditDTO);
                return Ok();
            }
            catch (Helpers.AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("addCategory")]
        public async Task<IActionResult> AddCategoryAsync( CategoryAddDTO categoryAddDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.AddCategoryAsync(categoryAddDTO);
                return Ok();
            }
            catch (Helpers.AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getCategoriesWithQuizes/{userId}")]
        public async Task<IActionResult> GetCategoriesWithQuizesAsync([FromRoute] int userId)
        {
            var categories = await _categoryService.GetCategoriesWithQuizesAsync(userId);
            return Ok(categories);
        }
    }
}