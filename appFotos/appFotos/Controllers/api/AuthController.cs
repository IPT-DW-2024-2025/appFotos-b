using appFotos.Areas.Identity.Pages.Account;
using appFotos.Models.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace appFotos.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("hello")]
        public ActionResult Hello()
        {
            return Ok("Hello");
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Authenticate(LoginApiModel loginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(loginRequest.Email);    
            }
            else
            {
                return BadRequest("Erro no Login");
            }
        }
        
        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return NoContent();
        }
    }
}
