using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Exctensions;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Infrastructure.Data;
//using UnitsTest.ApplicationCore.Builder;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Infrastructure.Data.Import;

namespace UnitsTest.ApplicationInfrastructure
{
	public class ImportDataTest
	{
		public ClubLogbookContext _context; 
		public void InitContext()
		{
			var dbOptions = new DbContextOptionsBuilder<ClubLogbookContext>()
			   .UseSqlServer("Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=Yossil_15;")
			   .Options;
			_context = new ClubLogbookContext(dbOptions);
		}
		public static ClubLogbookContext InitInMemoryContext()
		{

			var dbOptions = new DbContextOptionsBuilder<ClubLogbookContext>()
			   .UseInMemoryDatabase(databaseName: "TestCatalog")
			   .Options;
			return  new ClubLogbookContext(dbOptions);
		}
		[Test]
		public void Import()
		{
			InitContext();
			FullLogBookInitNew.S0_ConvertCustomPropertyTypes(_context);
			FullLogBookInitNew.S01_ConvertFromMyflightBookAirplane(_context);
			FullLogBookInitNew.S2_CreateClubGeneral(_context, "4XCGC", "BAZ", SupportedClub.BAZ);
			//FullLogBookInitNew.S2_CreateClubGeneral(_context, "4XCGC", "BAZ1", SupportedClub.BAZ1);
			FullLogBookInitNew.S3_CreateLogBook(_context, "059828392");
			FullLogBookInitNew.S4_ConvertFromMyflightBookFlightCSV(_context, "059828392");
			FullLogBookInitNew.s5_ConverBuzClubAirplaneFlight(_context);
		}
		//[Test]
		//public void ImportMembers()
		//{
		//	var dbOptions = new DbContextOptionsBuilder<Membercontext>()
		//	   .UseInMemoryDatabase(databaseName: "TestCatalog")
		//	   .Options;
		//	Membercontext context = new Membercontext(dbOptions);
		//	Pilot a = new Pilot();
		//	Pilot p = new Pilot();
		//	context.Members.Add(a);
		//	context.Members.Add(p);
		//	context.SaveChanges();
		//	IEnumerable<Member> c = context.Members;
		//}
	}
}
