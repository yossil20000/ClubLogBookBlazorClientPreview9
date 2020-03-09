using ClubLogBook.Client.Services.Contracts;
using ClubLogBook.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ClubLogBook.Application.Models;

namespace ClubLogBook.Client.States
{
	public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private UserInfo _userInfoCache;
        private readonly IAuthorizeApi _authorizeApi;

        public IdentityAuthenticationStateProvider(IAuthorizeApi authorizeApi)
        {
            this._authorizeApi = authorizeApi;
        }

        public async Task Login(LoginParameters loginParameters)
        {
            await _authorizeApi.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Register(RegisterParameters registerParameters)
        {
            await _authorizeApi.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await _authorizeApi.Logout();
            _userInfoCache = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<UserInfo> GetUserInfo()
        {
            if (_userInfoCache != null && _userInfoCache.IsAuthenticated) return _userInfoCache;
            _userInfoCache = await _authorizeApi.GetUserInfo();
            return _userInfoCache;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetUserInfo();
                if (userInfo.IsAuthenticated)
                {
					//var claims = new[] { new Claim(ClaimTypes.Name, _userInfoCache.UserName) }.Concat(_userInfoCache.ExposedClaims.Select(c => new Claim(c.Key, c.Value)));
					List<Claim> claims = new List<Claim>();
					foreach(var i in _userInfoCache.ExposedClaims)
					{
						foreach(var ii in i.Value)
						{
							claims.Add(new Claim(i.Key, ii));
						}
					}
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
		public async Task<List<string>> GetRoles()
		{
			var result = await _authorizeApi.GetRoles();
			return result;
		}
		public async Task<List<string>> GetPolicies()
		{
			var result = await _authorizeApi.GetPolicies();
			return result;
		}
		public async Task<List<AdminUserInfo>> GetUsers()
		{
			var results = await _authorizeApi.GetUsers();
			return results;
		}
		public async Task AddNewUser(AdminUserInfo user)
		{
			 await _authorizeApi.AddUser(user);
			
		}
		public async Task SaveUser(AdminUserInfo user)
		{
			await _authorizeApi.SaveUser(user);
		}
		public async Task EditUser(AdminUserInfo user)
		{
			await _authorizeApi.EditUser(user);
		}
		public async Task DeleteUser(AdminUserInfo user)
		{
			await _authorizeApi.DeleteUser(user);
		}
		public async Task<AdminUserInfo> CurrentUser()
		{
			var result = await _authorizeApi.CurrentUser();
			return result;
		}
		public async Task<List<PilotSelectModel>> GetPilots()
		{
			var result = await _authorizeApi.GetPilots();
			return result;
		}
	}
}
