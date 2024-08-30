//using Logic.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using PlatF.Model.Dto.Category;

//namespace WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoriesController : ControllerBase
//    {
//        private ICategoryService _categoryService;

//        public CategoriesController(ICategoryService categoryService)
//        {
//            _categoryService = categoryService;
//        }

//        [HttpGet, Authorize]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDto>))]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> GetAllAssync()
//        {
//            try
//            {
//                return Ok(await _categoryService.GetAllAsync());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpGet("{id:int}"), Authorize]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> GetById(int id)
//        {
//            try
//            {
//                return Ok(await _categoryService.GetByIdAsync(id));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpPut(), Authorize]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Update(CategoryDto category)
//        {
//            try
//            {
//                return Ok(await _categoryService.Update(category));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//    }
//}
