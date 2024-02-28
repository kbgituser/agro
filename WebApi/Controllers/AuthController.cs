using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlatF.Model.Data;
using PlatF.Model.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ITokenService _tokenService;

        public AuthController(ApplicationDbContext applicationDbContext, ITokenService tokenService)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost, Route("login")]
        //public async IActionResult Login([FromBody] LoginModel loginModel)
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            //if (loginModel is null)
            //{
            //    return BadRequest("Invalid client request");
            //}

            //var user = _applicationDbContext.LoginModels.FirstOrDefault(u =>
            //    (u.UserName == loginModel.UserName) && (u.Password == loginModel.Password));
            //if (user is null)
            //    return Unauthorized();

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, loginModel.UserName),
            //    new Claim(ClaimTypes.NameIdentifier, loginModel.UserName),
            //    new Claim(ClaimTypes.Role, "Manager")
            //};
            //var accessToken = _tokenService.GenerateAccessToken(claims);
            //var refreshToken = _tokenService.GenerateRefreshToken();

            //user.RefreshToken = refreshToken;
            //user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            //_applicationDbContext.SaveChanges();

            //return Ok(new AuthenticatedResponse
            //{
            //    Token = accessToken,
            //    RefreshToken = refreshToken
            //});

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

