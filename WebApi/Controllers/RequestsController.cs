﻿using Agro.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatF.Model.Dto.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : Controller
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IntentionDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(IntentionDto requestDto)
        {
            try
            {
                await _requestService.Create(requestDto);
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
            return Ok(await _requestService.GetAllAsync());
        }

        //[HttpGet(), Authorize]
        [HttpGet(), Authorize]
        [Route("ByPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IntentionDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByPageAsync(int? id = 1)
        {
            return Ok(await _requestService.GetAllPagedAsync(1));
        }

        [HttpGet("{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IntentionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRequestById(int id)
        {
            try
            {
                return Ok(await _requestService.GetRequestByIdAsync(id));
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
                await _requestService.Update(requestDto);
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
                await _requestService.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
