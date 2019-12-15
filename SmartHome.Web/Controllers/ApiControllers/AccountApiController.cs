using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.ServiceInterfaces.Account;
using SmartHome.DomainCore.ServiceInterfaces.User;

namespace SmartHome.Web.Controllers.ApiControllers
{
    [Authorize]
    public class AccountApiController : Controller
    {
        private readonly ISignInService signInService;
        private readonly IGetUsersService getUsersService;

        public AccountApiController(ISignInService signInService, IGetUsersService getUsersService)
        {
            this.signInService = signInService;
            this.getUsersService = getUsersService;
        }

        [AllowAnonymous]
        [HttpPost("api/account/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
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
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VeryPrivateKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("SmartHome",
                user.UserName,
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}