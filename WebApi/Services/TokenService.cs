using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PlatF.Model.Data;
using PlatF.Model.Entities;
using PlatF.Model.Exceptions;
using PlatF.Model.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public TokenService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:7272",
                audience: "https://localhost:7272",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }



        public async Task<AuthenticatedResponse> Login(LoginModel loginModel)
        {
            if (loginModel is null)
            {
                throw new LoginException("Login and password are empty");
            }

            var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = _applicationDbContext.LoginModels.FirstOrDefault(u =>
                (u.UserName == loginModel.UserName) 
                //&& (u.Password == loginModel.Password)                
                );

                var claims = new List<Claim>
                {
                    //new Claim(ClaimTypes.Name, loginModel.UserName),
                    //new Claim(ClaimTypes.NameIdentifier, loginModel.UserName),
                    new Claim("Username", loginModel.UserName),

                    new Claim("Role", "Manager"),
                    new Claim("Role", "User")

                };
                var accessToken = GenerateAccessToken(claims);
                var refreshToken = GenerateRefreshToken();


                if (user == null)
                {
                    user = new LoginModel()
                    {
                        UserName = loginModel.UserName
                    };
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                    _applicationDbContext.LoginModels.Add(user);
                }
                else
                {
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                }

                _applicationDbContext.SaveChanges();

                return (new AuthenticatedResponse
                {
                    Token = accessToken,
                    RefreshToken = refreshToken
                });

            }
            throw new LoginException("Login or password is incorrect!");

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
        }
    }
}
