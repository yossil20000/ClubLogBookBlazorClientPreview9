using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.Specifications;
namespace ClubLogBook.Application.Services
{
	public class MemberService : IMemberService
	{
		private readonly IMemberRepository _memberRepository;
		public MemberService(IMemberRepository memberRepository)
		{
			_memberRepository = memberRepository;
		}
		public async Task<IEnumerable<Pilot>> GetAllPilotNotInClub()
		{
			return await _memberRepository.GetAllPilotNotInClub();
		}
		public Task<bool> IsExist(Pilot pilot)
		{
			return _memberRepository.IsExist(pilot );
		}
		public async Task<Pilot> AddPilot(Pilot pilot)
		{
			
			
			return await _memberRepository.AddAsync(pilot);
		}
		public async Task UpdatePilot(Pilot pilot)
		{
			await _memberRepository.UpdateAsync(pilot);
		}
		public async Task<Pilot> GetPilotById(int id)
		{
			return await _memberRepository.GetByIdAsync(id);
		}
		public async Task DeletePilotById(int id)
		{

			await _memberRepository.DeletePilot(id);
		}

		public async Task<IEnumerable<Pilot>> GetAllPilot()
		{
			return await _memberRepository.GetAllPilots();
		}
		public async Task UpdatePilotUserId(int id, Guid userId)
		{
			Pilot p = await _memberRepository.GetByIdAsync(id);
			if(p!=null)
			{
				await RemoveUserId(userId);
				p.UserId = userId;
				await _memberRepository.UpdateAsync(p);
			}
			
		}
		public async Task RemoveUserId(Guid userId)
		{
			var members = await _memberRepository.ListAllAsync();
			foreach(var m in members)
			{
				if(m.UserId == userId)
				{
					m.UserId = Guid.Empty;
					await _memberRepository.UpdateAsync(m);
				}
				
			}
		}
	}
}
