﻿using Agro.Logic.Interfaces;
using Agro.Model.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Agro.Model.Dto.Intention;

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

        //[HttpPost, Authorize]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IntentionDto>))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> CreateAsync(RequestDto requestDto)
        //{
        //    try
        //    {
        //        await _requestService.CreateAsync(requestDto);
        //        return Ok(requestDto.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RequestDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _requestService.GetAllAsync());
        }

        [HttpGet(), Authorize]
        [Route("ByPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RequestDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByPageAsync(int? id = 1)
        {
            return Ok(await _requestService.GetAllRequestDtoPagedAsync(1));
        }

        [HttpGet("{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RequestDto))]
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
        public async Task<IActionResult> Update(RequestDto requestDto)
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

        [HttpDelete("{id:int}"), Authorize("Admin")]
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

        [HttpPut("{id:int}"), Authorize("Business")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IntentionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReleaseById(int id)
        {
            try
            {
                await _requestService.ReleaseAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("assign/id")]
        [HttpPut(), Authorize("Business")]

        //[HttpPut("{id:int}"), Authorize("Business")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IntentionDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignToPerformer(int id)
        {
            try
            {
                await _requestService.ReleaseAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
