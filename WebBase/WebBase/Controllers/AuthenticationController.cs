using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebBase.Models;

namespace WebBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: api/Authentication
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {

            ApplicationUser appuser = await _userManager.FindByEmailAsync(ConfigurationManager.AppSetting["User"]);
            var token = BuildToken(appuser);
            return Ok(token);
        }

        // GET: api/Authentication/5
        [Authorize]
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var email = HttpContext.User.Identity;
            var userDb = await _userManager.FindByEmailAsync(email.Name);
            return Ok(userDb);
        }

        // POST: api/Authentication
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            var userDb = await _userManager.GetUserAsync(HttpContext.User);
            return Ok(userDb);
        }

        // Build Token for Login and Create
        private IActionResult BuildToken(ApplicationUser userInfo)
        {

            try
            {
                // build claims
                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email ),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // buuild key and credentials
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["Secret_Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // create token 
                var expiration = DateTime.UtcNow.AddHours(1);
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "yourdomain.com",
                    audience: "yourdomain.com",
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = expiration
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
