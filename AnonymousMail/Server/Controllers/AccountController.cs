using AnonymousMail.Server.Services;
using AnonymousMail.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AnonymousMail.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;

        public AccountController(AccountService service)
        {
            _service = service;
        }

        [HttpGet("GetCurrentUser")]
        public ActionResult<User> GetCurrentUser()
         {
            if (User.Identity!.IsAuthenticated)
            {
                return Ok(new User()
                {
                    Id = User.Claims.First(t => t.Type == ClaimTypes.NameIdentifier).Value,
                    Username = User.Identity!.Name!
                });
            }

            return Unauthorized("Пользователь не авторизован");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] string username)
        {
            var user = await _service.CreateOrGetUserAsync(username);


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            var claimIdentity = new ClaimsIdentity(claims, "MailUser");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("MailUser", claimPrincipal);

            return Ok("Пользователь вошел в систему");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(User.Identity!.AuthenticationType);
            return Ok("Пользователь вышел из системы");
        }

    }
}
