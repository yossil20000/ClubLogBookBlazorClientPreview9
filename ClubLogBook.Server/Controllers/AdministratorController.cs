using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClubLogBook.Application.Services;
using ClubLogBook.Core.Entities;
using ClubLogBook.Infrastructure.Data;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.Models;

using ClubLogBook.Server.Services;
using ClubLogBook.Server.Models;
using ClubLogBook.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using MediatR;
using ClubLogBook.Application.AdministratorManager.Commands.Update;
using ClubLogBook.Application.AdministratorManager.Commands;
using ClubLogBook.Application.ClubContact.Queries;
using ClubLogBook.Application.Common.Models;
using ClubLogBook.Application.AdministratorManager.Queries;
using ClubLogBook.Shared.ApiModel;
namespace ClubLogBook.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	
	public class AdministratorController : ControllerBase
    {

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;
		private IPasswordHasher<ApplicationUser> _passwordHasher;
		private readonly IMapper _mapper;
		//private readonly IClubContactsViewModelService _clubContactsViewModelService;
		//IClubService _clubService;
		//IMemberService _memberService;
		IMediator _mediator;
		public AdministratorController(IMediator mediator, IMemberService memberService, IClubContactsViewModelService clubContactsViewModelService, IClubService clubService/*, IMapper mapper*/, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager, IPasswordHasher<ApplicationUser> passwordHasher)
		{
			_mediator = mediator;
			_userManager = userManager;_signInManager = signInManager; _roleManager = roleManager;
			/*_memberService = memberService;_clubContactsViewModelService = clubContactsViewModelService;_clubService = clubService;*//*_mapper = mapper;*/
			_passwordHasher =passwordHasher;
			var configuration = new MapperConfiguration(cfg => {
				cfg.AddProfile(new MappingProfile());
			});
			_mapper = configuration.CreateMapper();
		}
		////// GET: api/Contact
		////[HttpGet]
		////public IEnumerable<string> Get()
		////{
		////	return new string[] { "value1", "value2" };
		////}

		////// GET: api/Contact/5
		////[HttpGet("{id}", Name = "GetId")]
		////public string GetId(int id)
		////{
		////	return "value";
		////}

		// POST: api/Contact
		[HttpPost]
		public async Task<IActionResult> AddUser(AdminUserInfo user)
		{
			
			
			return await AddNewUser(user);
		}
		[HttpPost]
		public async Task<IActionResult> EditUser(AdminUserInfo adminUserInfo)
		{
			var user = await _userManager.FindByIdAsync(adminUserInfo.Id);
			if (user != null)
			{
				if(!string.IsNullOrEmpty(adminUserInfo.Password))
				{
					user.PasswordHash = _passwordHasher.HashPassword(user, adminUserInfo.Password);
				}
				if(!string.IsNullOrEmpty(adminUserInfo.Email))
				{
					user.Email = adminUserInfo.Email;
				}
				if( !string.IsNullOrEmpty(adminUserInfo.Email))
				{
					IdentityResult identityResult = await _userManager.UpdateAsync(user);
					if (!identityResult.Succeeded)
					{
						var error = identityResult.Errors.FirstOrDefault().Description;
						return StatusCode(10000);
					}
					await UpdateUserRole(user, adminUserInfo.Roles);
					UpdateUserIdCommand updateUserIdCommand = new UpdateUserIdCommand(adminUserInfo.PilotId, user.Id.ToString());
					int result = await _mediator.Send(updateUserIdCommand);
					//await _memberService.UpdatePilotUserId(adminUserInfo.PilotId, user.Id.ToString());

					return result >=0 ? Ok(): StatusCode(10003);
				}
				return StatusCode(10002);
			}
			return StatusCode(10001);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteUser(AdminUserInfo adminUserInfo)
		{
			var user = await _userManager.FindByIdAsync(adminUserInfo.Id);
			if(user != null)
			{
				
				IdentityResult identityResult = await _userManager.DeleteAsync(user);
				if (!identityResult.Succeeded)
				{
					var error = identityResult.Errors.FirstOrDefault().Description;
					return StatusCode(10000);
				}
				DeleteUserIdCommand deleteUserIdCommand = new DeleteUserIdCommand(adminUserInfo.PilotId, user.Id.ToString());
				int result = await _mediator.Send(deleteUserIdCommand);

				return result >= 0 ? Ok() : StatusCode(10003);
			}
			return StatusCode(10001);

		}
	
		[HttpPost]
		public async Task<IActionResult> SaveUser(AdminUserInfo adminUserInfo)
		{
			var user = await _userManager.FindByIdAsync(adminUserInfo.Id);
			if (user != null)
			{
				IdentityResult identityResult = await _userManager.UpdateAsync(user);
				if (!identityResult.Succeeded)
				{
					var error = identityResult.Errors.FirstOrDefault().Description;
					return StatusCode(10000);
				}
				return Ok();
			}
			return StatusCode(10001);
		}
		// PUT: api/Contact/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
		[HttpGet]
		public List<string> GetRoles()
		{
			return IdentityInfo.IdentityRoles;
		}
		[HttpGet]
		public List<string> GetPolicies()
		{
			
			return IdentityInfo.IdentityPolicies;
		}
		[HttpGet]
		public async Task<List<PilotSelectModel>> GetPilots()
		{
			List<PilotSelectModel> pilotSelectModel= new List<PilotSelectModel>();
			GetClubMembersListQuery getClubMembersListQuery = new GetClubMembersListQuery(QueryBy.Name,  "",  0);

			var pilotSelectListModel = await _mediator.Send(getClubMembersListQuery);
			pilotSelectModel.AddRange(pilotSelectListModel.PilotSelectList);

			return pilotSelectModel;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(OperationResponse<AdminUserInfo>))]
		public async Task<IActionResult> CurrentUser()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			 
			AdminUserInfo adminUserInfo = _mapper.Map<ApplicationUser, AdminUserInfo>((user));
			return Ok(new OperationResponse<AdminUserInfo> { IsSuccess = true,Message = $"{user.Id} has been found", Record=adminUserInfo}
				);
		}
		[HttpGet]
		public async Task<List<AdminUserInfo>> GetUsers()
		{
			
			//await EnsureAdmin();
			var users =  _userManager.Users.ToList();
			
			List<AdminUserInfo> adminUserInfos = _mapper.Map<List<ApplicationUser>, List<AdminUserInfo>>(users);
			foreach(var user in users)
			{
				GetPilotByUserIdQuery getPilotByUserIdQuery = new GetPilotByUserIdQuery(user.Id.ToString());
				var pilot = await _mediator.Send(getPilotByUserIdQuery);
				var admin = adminUserInfos.Find(ad => ad.Id == user.Id.ToString());
				

				if(admin != null)
				{
					if (pilot != null)
						admin.MemberDescription = $"{pilot.FirstName} ({pilot.IdNumber})";
					try
					{
						var roles = await _userManager.GetRolesAsync(user);
						admin.Roles = roles.ToList<string>();
						System.Diagnostics.Debug.WriteLine(admin.GetformatedRoles());
					}
					catch(Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
					}
					
				}
				
			}
			//var rng = new Random();
			//return  Enumerable.Range(1, 5).Select(index => new AdminUserInfo()).ToList<AdminUserInfo>();
			
			return adminUserInfos;
		}
		protected async Task RemoveUserFromeRoles(ApplicationUser user)
		{
			foreach(var role in Enum.GetValues(typeof( IdentityInfo.Roles)))
			{
				var UserResult = await _userManager.IsInRoleAsync(user, role.ToString());
				if (UserResult)
				{
					// Put admin in Administrator role
					await _userManager.RemoveFromRoleAsync(user, role.ToString());
				}
			}
		}
		protected async Task EnsureAdmin()
		{
			// ensure there is a ADMINISTRATION_ROLE
			AdminUserInfo adminUserInfo = new AdminUserInfo();
			adminUserInfo.Roles.Add(IdentityInfo.Roles.Administrators.ToString());
			adminUserInfo.UserName = "Administrator";
			adminUserInfo.Email = "Admin@BazHaifa.co.il";
			adminUserInfo.Password = "Admin@BazHaifa";

			await AddNewUser(adminUserInfo);
		}
		protected async Task AddRole(ApplicationUser user, string role)
		{
			var UserResult = await _userManager.IsInRoleAsync(user, role);
			if (!UserResult)
			{
				// Put admin in Administrator role
				await _userManager.AddToRoleAsync(user, role);
			}
		}
		protected async Task CreateRole(string role)
		{
			var RoleResult = await _roleManager.FindByNameAsync(role);
			if (RoleResult == null)
			{
				IdentityResult result = await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
				if (result.Succeeded)
				{

				}
			}
		}
		protected async Task UpdateUserRole(ApplicationUser user,List<string> roles)
		{
			await RemoveUserFromeRoles(user);
			foreach (string role in roles)
			{
				await CreateRole(role);

				await AddRole(user, role);
			}
		}
		protected async Task<IActionResult> AddNewUser(AdminUserInfo adminUserInfo)
		{
			// ensure there is a ADMINISTRATION_ROLE
			
			
			// Ensure a user named Admin@BlazorHelpWebsite.com is an Administrator
			var user = await _userManager.FindByEmailAsync(adminUserInfo.Email);
			IdentityResult results;
			if (user == null)
			{
				user = new ApplicationUser();
				user.UserName = adminUserInfo.UserName;
				user.Email = adminUserInfo.Email;
				 results = await _userManager.CreateAsync(user, adminUserInfo.Password);
				if (results.Succeeded)
				{
					await UpdateUserRole(user, adminUserInfo.Roles);
					UpdateUserIdCommand updateUserIdCommand = new UpdateUserIdCommand(adminUserInfo.PilotId, user.Id.ToString());
					int result = await _mediator.Send(updateUserIdCommand);
					//await _memberService.UpdatePilotUserId(adminUserInfo.PilotId, user.Id.ToString());
				}
			}
			else
			{
				
			}
			
			return Ok();
		}
	}
}
