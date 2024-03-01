using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatF.Model.Dto.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IIntentionService _intentionService;

        public RequestsController(IIntentionService requestService)
        {
            _intentionService = requestService;
        }

        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IntentionDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(IntentionDto requestDto)
        {
            try
            {
                await _intentionService.Create(requestDto);
                return Ok(requestDto.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IntentionDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _intentionService.GetAllAsync());
        }

        //[HttpGet(), Authorize]
        [HttpGet()]
        [Route("ByPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IntentionDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByPageAsync(int? id = 1)
        {
            return Ok(await _intentionService.GetAllPagedAsync(1));
        }

        [HttpGet("{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IntentionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRequestById(int id)
        {
            try
            {
                return Ok(await _intentionService.GetRequestByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut(), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(IntentionDto requestDto)
        {
            try
            {
                await _intentionService.Update(requestDto);
                return Ok(requestDto.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IntentionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _intentionService.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
