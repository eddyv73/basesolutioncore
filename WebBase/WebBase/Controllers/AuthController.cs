using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBase.Models;

namespace WebBase.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /// <summary>Index
        /// <para>This example you can see, the get user with _userManager and Login With Password</para>
        /// <seealso cref="Index"/>
        /// </summary>
        public async Task<IActionResult> Index()
        {
            //Get User with usermanager
            var user = await _userManager.FindByEmailAsync(ConfigurationManager.AppSetting["User"]);
            //Sign in with SignManager

            var isLoged = await _signInManager.PasswordSignInAsync(user, ConfigurationManager.AppSetting["Pwd"], true, false);

            //If is true you are in
            return View();
        }

    }
}