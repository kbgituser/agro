using Agro.Model.Data;
using Agro.Model.Dto.User;
using Agro.Model.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController( ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] Login loginModel)
        {
            try
            {
                var result = await _tokenService.Login(loginModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //var result = _tokenService.Login(loginModel).Result;
        }
    }
}

