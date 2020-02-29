using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Infrastructure.Persistence;

namespace ClubLogBook.Infrastructure.Data
{
	public class ClubRepository : EFRepository<Club>, IClubRepository
	{
		public ClubRepository(ApplicationDbContext dbcontex) : base(dbcontex)
		{
		}

		public Club GetAllByIdAsync(int id)
		{
			//Task<Club> club;
			var membersVar = _dbContex.Set<Pilot>();
			List<Pilot> members1 = _dbContex.Members.ToList();
			//List<EMAIL> eMAILs = _dbContex.EMAILs.ToList();
			//List<Phone> phones = _dbContex.Phones.ToList();
			List<Club> list2 = _dbContex.Clubs.Include(a => a.Members).ThenInclude(a => a.Contact).ToList();

			List<Club> list = _dbContex.Clubs.ToList();
			//List<Club> list = _dbContex.Clubs.Include(a => a.Aircrafts).ThenInclude(a => a.AirCraftModel).ToList();
			//var aclub = _dbContex.Clubs.Where(x => x.Id == id).AsNoTracking();
			//List<Member> members = _dbContex.Members.ToList<Member>();
			//var phone = _dbContex.Phones.AsNoTracking();
			//var emails = _dbContex.EMAILs.AsNoTracking();
			//var m =  _dbContex.Members.AsNoTracking();

			return list.FirstOrDefault();
			
		}
		public  async Task<Club> GetAllByIdAsync(ISpecification<Club> spec)
		{
			var club1 = await ListAsync(spec);
			//var membersVar = _dbContex.Members;
			//List<Pilot> members1 = _dbContex.Members.ToList();

			////List<Phone> phones = _dbContex.Phones.ToList();
			//List<Club> list2 = _dbContex.Clubs.Include(a => a.Members).ThenInclude(a => a.Contact).ToList();

			//List<Club> list = _dbContex.Clubs.Include(a => a.Aircrafts).ThenInclude(a => a.AirCraftModel).ToList();
			//var aclub = _dbContex.Clubs.Where(x => x.Id == club1.Id).AsNoTracking();
			//List<Member> members = _dbContex.Members.ToList<Member>();
			////var phone = _dbContex.Phones.AsNoTracking();
			////var emails = _dbContex.EMAILs.AsNoTracking();
			//var m = _dbContex.Members.AsNoTracking();

			//return list.FirstOrDefault();
			return club1.FirstOrDefault();

		}


	}
}
