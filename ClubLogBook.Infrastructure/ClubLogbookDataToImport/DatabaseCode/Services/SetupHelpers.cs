using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ClubLogBook.Infrastructure.Data.Import;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Infrastructure.Data.Services
{
	public enum DbStartupMode { UseExisting,EnsureCreated,EnsureDeletedCreated,UseMigrations}
	public static class SetupHelpers
	{
		public const string DefaultUserId = "yos.1965@gmail.com";
		public const string SeedDataSearchName = "";
		public const string SeedFileSubDirectory = "";

		private static readonly string[] DummyUsersIds = new[] {DefaultUserId, "admin@baz.co.il"};
		public static void DevelopmentEnsureDeleted(this ClubLogbookContext db)
		{
			db.Database.EnsureDeleted();
		}
		public static void DevelopmentEnsureCreated(this ClubLogbookContext db)
		{
			db.Database.EnsureCreated();
		}

		public static int SeedDatabase(this ClubLogbookContext db,string dataDirectory,bool createNew = false)
		{
			
			if (!(db.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
				throw new InvalidAsynchronousStateException("the database does not exist. If you are using Migration then Run PMC command update-database to create it");
			
				
            if(db.CustomPropertyTypes.Count() ==0)
            {
               // FullLogBookInitNew.S0_ConvertCustomPropertyTypes(db);
            }
            if(db.AirCraftModels.Any() == false)
            {
                //FullLogBookInitNew.S01_ConvertFromMyflightBookAirplane(db);
            }
            if(db.Clubs.Count() ==0)
            {
                FullLogBookInitNew.S2_CreateClubGeneral(db,"4XCGC","BAZ",SupportedClub.BOTH);
            }
            /*
            if(FullLogBookInitNew.S3_CreateLogBook(db, 059828392))
            {
                FullLogBookInitNew.S4_ConvertFromMyflightBookFlightCSV(db, 059828392);
            }
			*/
            //FullLogBookInitNew.s5_ConverBuzClubAirplaneFlight(db);
			if (createNew)
			{
				FullLogBookInitNew.UpdatePilotDefaultPhoto(db);
				FullLogBookInitNew.UpdateairplaneDefaultPhoto(db);
				return 0;
			}
			//var numPilots = db.Pilots.Count();
			//if (numPilots == 0)
			//{
			//             //Code to fill data base data
			//             FullLogBookInitNew.ConvertLogBook(db);
			//}
			return 0;
		}
	}
}
