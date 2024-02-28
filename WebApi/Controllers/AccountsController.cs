using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlatF.Model.Dto;
using PlatF.Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using WebApi.JwtFeatures;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly JwtHandler _jwtHandler;
        private UserManager<ApplicationUser> _userManager;

        public AccountsController(JwtHandler jwtHandler, UserManager<ApplicationUser> userManager)
        {
            _jwtHandler = jwtHandler;
            _userManager = userManager;
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForRegistrationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        [HttpGet, Authorize]
        //[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }
    }
}
