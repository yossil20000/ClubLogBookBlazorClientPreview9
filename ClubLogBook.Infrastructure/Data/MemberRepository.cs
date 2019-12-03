using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Infrastructure.Persistence;

namespace ClubLogBook.Infrastructure.Data
{
	public class MemberRepository : EFRepository<Pilot>, IMemberRepository
	{
		public  MemberRepository(ApplicationDbContext dbcontex) : base(dbcontex)
		{
		}
		public async Task<bool> IsExist(Pilot pilot)
		{
			var members = await ListAllAsync();
			var memberExist = members.Where(i => i.Equals(pilot));
			if (memberExist.Any())
				return true;
			else
				return false;
		}
		public async Task<int> AddPilot(Pilot pilot)
		{
			var contactBook = _dbContex.ContactBooks.Where(i => i.Name == "GENERAL").FirstOrDefault();
			contactBook.AddContact(pilot.Contact);

			_dbContex.ContactBooks.UpdateRange(contactBook);
			_dbContex.Members.Update(pilot);

			return await _dbContex.SaveChangesAsync();
			
		}
		public async Task<Pilot> GetPilot(int id)
		{
			return await _dbContex.Members.FindAsync(id);
		}
		public async Task DeletePilot(int id)
		{
			var p = await _dbContex.Members.Include(c => c.Contact).ThenInclude(a => a.Addresses).Include(c => c.Contact).ThenInclude(a => a.Phones).Include(c => c.Contact).ThenInclude(a => a.EMAILs).Where(m => m.Id == id).FirstOrDefaultAsync();
			
			_dbContex.Addresses.RemoveRange(p.Contact.Addresses);
			_dbContex.EMAILs.RemoveRange(p.Contact.EMAILs);
			_dbContex.Phones.RemoveRange(p.Contact.Phones);
			_dbContex.Members.Remove(p);
			
			await _dbContex.SaveChangesAsync();
		}

		public async Task<IEnumerable<Pilot>> GetAllPilotNotInClub()
		{
			var clubs = _dbContex.Clubs.ToList();
			List<Pilot> memeberPilot = _dbContex.Members.ToList();
			List<Pilot> pilots = new List<Pilot>();
			foreach(var p in clubs )
			{
				foreach(var pp in p.Members)
				{
					pilots.Add(pp);
				}
			}
			
			return memeberPilot.Except(pilots);
		}
		public async Task<IEnumerable<Pilot>> GetAllPilots()
		{

			var memeberPilot = await _dbContex.Members.Include(c => c.Contact).ThenInclude(a => a.Addresses).Include(c => c.Contact).ThenInclude(a => a.Phones).Include(c => c.Contact).ThenInclude(a => a.EMAILs).ToListAsync();


			return memeberPilot;
		}
	}
}
