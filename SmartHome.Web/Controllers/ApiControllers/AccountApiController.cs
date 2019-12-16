using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Account;
using SmartHome.DomainCore.ServiceInterfaces.User;
using SmartHome.Web.Configurations;

namespace SmartHome.Web.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountApiController : Controller
    {
        private readonly ISignInService signInService;
        private readonly IGetUsersService getUsersService;
        private readonly JwtConfiguration jwtConfiguration;

        public AccountApiController(ISignInService signInService, IGetUsersService getUsersService, JwtConfiguration jwtConfiguration)
        {
            this.signInService = signInService;
            this.getUsersService = getUsersService;
            this.jwtConfiguration = jwtConfiguration;
        }

        [AllowAnonymous]
        [HttpPost("api/account/login")]
        public async Task<IActionResult> Login([Bind(Prefix = "")] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid passed model.");
            }
            
            var result = await signInService.SignInAsync(model);
            if (!result.Succeeded)
            {
                return BadRequest(result.ValidationResult.ToString());
            }

            var user = await getUsersService.GetByIdAsync(result.Value);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var token = new JwtSecurityToken(jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfiguration.ValidInMinutes),
                signingCredentials: new SigningCredentials(jwtConfiguration.SecurityKey,
                    SecurityAlgorithms.HmacSha256));

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}