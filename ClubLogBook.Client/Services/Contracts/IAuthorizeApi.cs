using ClubLogBook.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Application.ViewModels;

namespace ClubLogBook.Client.Services.Contracts
{
	public interface IAuthorizeApi
    {
        Task Login(LoginParameters loginParameters);
        Task Register(RegisterParameters registerParameters);
        Task Logout();
        Task<AdminUserInfo> GetUserInfo();
		Task<List<string>> GetPolicies();
		Task<List<string>> GetRoles();
		Task<List<AdminUserInfo>> GetUsers();
		Task AddUser(AdminUserInfo user);
		Task SaveUser(AdminUserInfo user);
		Task EditUser(AdminUserInfo user);
		Task DeleteUser(AdminUserInfo user);
		Task<AdminUserInfo> CurrentUser();
		Task<List<PilotSelectViewModel>> GetPilots();
	}
}
