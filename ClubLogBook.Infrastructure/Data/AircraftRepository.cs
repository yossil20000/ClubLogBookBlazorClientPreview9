using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Common;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ClubLogBook.Infrastructure.Data
{
	public class AircraftRepository : EFRepository<Aircraft> , IAircraftRepository
	{

		public AircraftRepository(ApplicationDbContext dbcontex) : base(dbcontex)
		{

		}

		public async  Task<List<Aircraft>> GetAllInClub(string clubName)
		{
			var list = (_dbContex.Clubs.Where(club => club.Name == clubName).SelectMany(a => a.Aircrafts)).ToList();
			
			
			//		(select a	from _db .Products AS product WHERE product.ListPrice > @price1 ) 
			//   intersect(SELECT product FROM AdventureWorksEntities.Products AS

			//product WHERE product.ListPrice > @price2)
			//var a = await _dbContex.Aircrafts.Intersect(list).ToListAsync();
			//var intersect = a.Except(list).ToList();
			return list;
		}

		public async Task<List<Aircraft>> GetAllNotInClub(string clubName)
		{
			if (clubName == string.Empty)
			{
				var aircraftNotInAny = GetAllNotInAnyClub();
			}
			var aircraftNotInClub = await (_dbContex.Aircrafts).Except(_dbContex.Clubs.Where(club => club.Name == clubName).SelectMany(a => a.Aircrafts)).ToListAsync();
			return aircraftNotInClub;
		}
		public async Task<List<AircraftPrice>> GetAircraftPrices()
		{
			var a = await _dbContex.AircraftPrices?.ToListAsync();
			return a;
		}
		async Task<List<Aircraft>> GetAllNotInAnyClub()
		{
			return await  (_dbContex.Clubs.SelectMany(a => a.Aircrafts)).Intersect(_dbContex.Aircrafts).ToListAsync();
		}
	}
}
