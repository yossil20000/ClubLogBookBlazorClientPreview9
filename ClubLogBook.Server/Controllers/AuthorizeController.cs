using ClubLogBook.Server.Models;
using ClubLogBook.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubLogBook.Server.Controllers
{
	[Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;

		public AuthorizeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
			_roleManager = roleManager;
        }
		protected async Task EnsureAdmin()
		{
			// ensure there is a ADMINISTRATION_ROLE
			string admin = IdentityInfo.Roles.Administrators.ToString();
			var RoleResult = await _roleManager.FindByNameAsync(admin);
			if (RoleResult == null)
			{
				// Create ADMINISTRATION_ROLE Role


				IdentityResult result = await _roleManager.CreateAsync(new IdentityRole<Guid>(IdentityInfo.Roles.Administrators.ToString()));
				if (result.Succeeded)
				{

				}
			}

			// Ensure a user named Admin@BlazorHelpWebsite.com is an Administrator
			var user = await _userManager.FindByNameAsync("Administrator");
			if (user == null)
			{
				user = new ApplicationUser();

				user.UserName = "Administrator";

				user.Email = "Admin@BazHaifa.co.il";

				var createResults = await _userManager.CreateAsync(user, "Admin@BazHaifa");
				if (!createResults.Succeeded)
				{
					createResults = await _userManager.UpdateAsync(user);
					//await _userManager.DeleteAsync(user);
					//createResults = await _userManager.CreateAsync(user, user.PasswordHash);

				}
			}
			if (user != null)
			{


				// Is Admin@BlazorHelpWebsite.com in administrator role?
				var UserResult = await _userManager.IsInRoleAsync(user, IdentityInfo.Roles.Administrators.ToString());
				if (!UserResult)
				{
					// Put admin in Administrator role
					await _userManager.AddToRoleAsync(user, IdentityInfo.Roles.Administrators.ToString());
				}
			}
		}
		[HttpPost]
        public async Task<IActionResult> Login(LoginParameters parameters)
        {
			await EnsureAdmin();
            if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(state => state.Errors)
                                                                       .Select(error => error.ErrorMessage)
                                                                       .FirstOrDefault());

            var user = await _userManager.FindByNameAsync(parameters.UserName);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, parameters.RememberMe);

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterParameters parameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(state => state.Errors)
                                                                        .Select(error => error.ErrorMessage)
                                                                        .FirstOrDefault());

            var user = new ApplicationUser();
            user.UserName = parameters.UserName;
            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

            return await Login(new LoginParameters
            {
                UserName = parameters.UserName,
                Password = parameters.Password
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        public UserInfo UserInfo()
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            return BuildUserInfo();
        }


        private UserInfo BuildUserInfo()
        {
			UserInfo userInfo = new UserInfo();
			
			try
			{
				
				var claimes = User.Claims.GroupBy(g => g.Type);
				foreach(var i in claimes)
				{
					List<string> l = new List<string>();
					foreach (var ii in i)
					{
						l.Add(ii.Value);
					}
					userInfo.ExposedClaims.Add(i.Key,l);
				}
				userInfo.IsAuthenticated = User.Identity.IsAuthenticated;
				userInfo.UserName = User.Identity.Name;
				//userInfo.ExposedClaims = User.Claims
				//	//Optionally: filter the claims you want to expose to the client
				//	//.Where(c => c.Type == "test-claim")
				//	.ToDictionary(c => c.Type, c => c.Value);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}

			return userInfo;
        }

    }
}
