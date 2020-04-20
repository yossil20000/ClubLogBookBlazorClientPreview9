using ClubLogBook.Client.Services.Contracts;
using ClubLogBook.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ClubLogBook.Application.Models;

namespace ClubLogBook.Client.Services.Implementations
{
	public class AuthorizeApi : IAuthorizeApi
    {
        private readonly HttpClient _httpClient;

        public AuthorizeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Login(LoginParameters loginParameters)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(loginParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Authorize/Login", stringContent);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/Authorize/Logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterParameters registerParameters)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(registerParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Authorize/Register", stringContent);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<AdminUserInfo> GetUserInfo()
        {
            var result = await _httpClient.GetJsonAsync<AdminUserInfo>("api/Authorize/UserInfo");
            return result;
        }
		public async Task<List<string>> GetRoles()
		{
			var result = await _httpClient.GetJsonAsync<List<string>>("api/Administrator/GetRoles");
			return result;
		}
		public async Task<List<string>> GetPolicies()
		{
			var result = await _httpClient.GetJsonAsync<List<string>>("api/Administrator/GetPolicies");
			return result;
		}

		public async Task AddUser(AdminUserInfo user)
		{
			var stringContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
			var result = await _httpClient.PostAsync("api/Administrator/AddUser", stringContent);
			if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
			result.EnsureSuccessStatusCode();
		}
		public async Task DeleteUser(AdminUserInfo user)
		{
			var stringContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
			var result = await _httpClient.PostAsync("api/Administrator/DeleteUser", stringContent);
			if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
			result.EnsureSuccessStatusCode();
		}

		public async Task EditUser(AdminUserInfo user)
		{
			var stringContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
			var result = await _httpClient.PostAsync("api/Administrator/EditUser", stringContent);
			if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
			result.EnsureSuccessStatusCode();
		}
		public async Task SaveUser(AdminUserInfo user)
		{
			var stringContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
			var result = await _httpClient.PostAsync("api/Administrator/SaveUser", stringContent);
			if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
			result.EnsureSuccessStatusCode();
		}
		public async Task<List<AdminUserInfo>> GetUsers()
		{
			var result = await _httpClient.GetJsonAsync<List<AdminUserInfo>>("api/Administrator/GetUsers");
			return result;
		}
		public async Task<AdminUserInfo> CurrentUser()
		{
			var result = await _httpClient.GetJsonAsync<AdminUserInfo>("api/Administrator/CurrentUser");
			
			return result;
		}
		public async Task<List<PilotSelectModel>> GetPilots()
		{
			var result = await _httpClient.GetJsonAsync<List<PilotSelectModel>>("api/Administrator/GetPilots");
			return result;
		}
	}
}
