using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlatF.Model.Dto;
using PlatF.Model.Entities;
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
        public async Task<IActionResult> Login(
            [FromBody] UserForRegistrationDto userForAuthentication
        )
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

            if (
                user == null
                || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password)
            )
                return Unauthorized(
                    new AuthResponseDto { ErrorMessage = "Invalid Authentication" }
                );

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(
            [FromBody] UserForRegistrationDto userForAuthentication
        )
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

            if (user is not null)
            {
                return Conflict("User already exists");
            }

            var creationResult = await _userManager.CreateAsync(user);

            if (creationResult.Succeeded)
            {
                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var claims = _jwtHandler.GetClaims(user);
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
            }
            else
            {
                return BadRequest(
                    new AuthResponseDto
                    {
                        ErrorMessage = string.Join(
                            " |",
                            creationResult.Errors.Select(x => x.Description)
                        )
                    }
                );
            }
        }

        [HttpGet, Authorize]
        //[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }
    }
}
