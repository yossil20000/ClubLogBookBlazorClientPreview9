
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Internal;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.PixelFormats;


namespace ClubLogBook.Infrastructure.Data.Import
{
	
    public static class FullLogBookInitNew
	{
		
		//private bool ClearDB()
		//{
		//	try
		//	{
		//		FullContex dbFullContex = new FullContex(FullContex.GetLogBookDbName());
		//		if (dbFullContex != null)
		//		{

		//			return dbFullContex.Database.EnsureDeleted();
		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		Trace.WriteLine(e);
		//		return false;
		//	}
		//	return true;
		//}

		//[Test]
		public static  void ConvertLogBook(ApplicationDbContext db)
		{
			try
			{
				
				if (true)
				{
					//StreamReader fileNameSream = new StreamReader(thisAssembly.GetManifestResourceStream("FullLogBook.NetCore.DataResource.custompropertytypes.csv"));
					
					
					S0_ConvertCustomPropertyTypes(db);
					S01_ConvertFromMyflightBookAirplane(db);
                    S2_CreateClubGeneral(db,"4XCGC","BAZ",SupportedClub.BAZ);
					S2_CreateClubGeneral(db, "4XCGC", "BAZ1", SupportedClub.BAZ1);
					S3_CreateLogBook(db,"059828392");
					S4_ConvertFromMyflightBookFlightCSV(db,"059828392");
                    
                    s5_ConverBuzClubAirplaneFlight(db);
				}
				else
				{
					Trace.WriteLine("Failed CleareDB");
				}

			}
			catch (Exception e)
			{
				Trace.WriteLine(e);
			}
		}
        public static byte[] GetDefaultAirplanePhoto()
        {
            Byte[] photoByte = null;
            try
            {
               
                //photoByte = Fulllogbook.ServiceLayer.Properties.Resources.Airplane;

            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

            }
            return photoByte;
        }
        public static byte[] GetDefaultPilotPhoto(Gender gender)
        {
            byte[] photoByte = null;

            try
            {
                if (gender == Gender.Female)
                {
					//photoByte = Fulllogbook.ServiceLayer.Properties.Resources.PilotFemale;
                }
                else
                {
                  // photoByte = Fulllogbook.ServiceLayer.Properties.Resources.PilotMale;
                }


            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

            }
            return photoByte;
        }
        public static List<CustomPropertyType> S0_ConvertCustomPropertyTypes(ApplicationDbContext db)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            List<CustomPropertyType> cptList = new List<CustomPropertyType>();
            string[] row;
            try
            {
                StreamReader sr = new StreamReader(thisAssembly.GetManifestResourceStream("ClubLogBook.Infrastructure.ClubLogbookDataToImport.DataResource.custompropertytypes.csv"));
                //db)
                {
                    if (sr == null)
                        return cptList;
                    db.CustomPropertyTypes.Load();
                    cptList = db.CustomPropertyTypes.ToList();
                    foreach (var cpt in cptList)
                    {
                        db.CustomPropertyTypes.Remove(cpt);
                    }
                    db.SaveChanges();
                    string[] header = sr.ReadLine().Split(',');
                    while (!sr.EndOfStream)
                    {
                        CustomPropertyType cpt = new CustomPropertyType();
                        //= Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                         //string[] row = sr.ReadLine().Split(',');
                        
                        row = sr.ReadLine().Split(',');

                        if (row.Length == 7)
                        {
                            cpt.Title = row[1].Replace("\"", "");

                            cpt.SortKey = row[2].Replace("\"", "");
                            cpt.FormatString = row[3].Replace("\"", ""); ;
                            cpt.Type = Convert.ToInt16(row[4]);
                            cpt.Flags = Convert.ToInt32(row[5]);
                            if (row[6] != null)
                            {
                                cpt.Description = row[6].Replace("\"", "");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Error Num Of Field");
                            }

                            cptList.Add(cpt);
                            db.CustomPropertyTypes.UpdateRange(cpt);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Error Num Of Field");
                        }
                    }
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            return cptList;
        }
        public static  bool S01_ConvertFromMyflightBookAirplane(ApplicationDbContext db)
        {
            try
            {
                //using (db)
                {
                    byte[] defaultAirplanePhoto = GetDefaultAirplanePhoto();
                    db.AirCraftModels.Load();
                    if (db.AirCraftModels.Any() == false)
                    {
                        List<AirplanemodelImportcs> airCraftModel = LoadJson();
                        List<Aircraft> aircraft = new List<Aircraft>();


                        foreach (var acModel in airCraftModel)
                        {

                            db.AirCraftModels.Add(acModel);
                            db.SaveChanges();
                            Aircraft acAircraft = new Aircraft();
                            //AircraftPrice apPrice = new AircraftPrice();
                            //apPrice.TailNumber = acModel.TailNumber;
                            acAircraft.TailNumber = acModel.TailNumber;
                            switch (acModel.CategoryClass)
                            {
                                case "ASEL":
                                    acAircraft.AircraftCategory = AircraftCategory.Airplane;
                                    acAircraft.AircraftClass = AircraftClass.SingleEngineLand;
                                    break;
                                case "AMEL":
                                    acAircraft.AircraftCategory = AircraftCategory.Airplane;
                                    acAircraft.AircraftClass = AircraftClass.MultiEngineLand;
                                    break;
                                case "ASES":
                                    acAircraft.AircraftCategory = AircraftCategory.Airplane;
                                    acAircraft.AircraftClass = AircraftClass.SingleEngineSea;
                                    break;
                                case "AMES":
                                    acAircraft.AircraftCategory = AircraftCategory.Airplane;
                                    acAircraft.AircraftClass = AircraftClass.MultiEngineSea;
                                    break;
                            }
                            if (acModel.Model.Contains("PA28R"))
                            {
                                acAircraft.Complex = true;
                                acAircraft.Retractable = true;
                                acAircraft.ConstantSpeedProp = true;
                            }
                            if (acModel.Model.Contains("8KCAB"))
                            {
                                acAircraft.Complex = true;
                                acAircraft.TailWheel = true;
                            }
                            if (acModel.Model.Contains("PA44"))
                            {
                                acAircraft.Complex = true;
                                acAircraft.Retractable = true;
                                acAircraft.ConstantSpeedProp = true;

                            }
                            acAircraft.Flaps = true;
                            acAircraft.Photo = defaultAirplanePhoto;
                            acAircraft.AirCraftModel = acModel;
                            db.Aircrafts.Add(acAircraft);
                            //db.AircraftPrices.Add(apPrice);
                            db.SaveChanges();
                        }

                    }
                    List<Aircraft> aircraftNoPhoto = db.Aircrafts.Where(ac => ac.Photo == null).ToList();
                    foreach (var ac in aircraftNoPhoto)
                    {
                        ac.Photo = defaultAirplanePhoto;
                        db.Aircrafts.UpdateRange(ac);
                    }
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;

        }
        public static bool S2_CreateBazClub(ApplicationDbContext db, SupportedClub supportedClub)
        {
            try
            {

                string clubTailNumber = "4XCGC";
                string clumName = "BAZ";
                if (db != null)
                {
                    List<Club> clubs = db.Clubs.ToList();
                    AircraftLogBook airplaneLogBook = null;
					ContactBook contactBook = null;
					Aircraft aircraft = db.Aircrafts.Where(ac => ac.TailNumber.ToUpper() == clubTailNumber).SingleOrDefault();

                    if (aircraft == null)
                        return false;
                    clubs = db.Clubs.ToList();
                    Club club = null;
                    if (clubs != null && clubs.Count > 0)
                        club = clubs?.Where(x => x.Name.ToUpper() == clumName).Single();
                    if (club == null)
                    {
                        club = new Club();
                        club.Name = "BAZ";
                        
                        club.AddAircraft(aircraft);
                    }
					
					var airplaneLogBooks = db.AircraftLogBooks.ToList();
					if (airplaneLogBooks != null && airplaneLogBooks.Count > 0)
					    airplaneLogBook = airplaneLogBooks.Where(x => x.TaiNumber.ToUpper() == aircraft.TailNumber.ToUpper()).Single();

					 if (airplaneLogBook == null)
					 {
					     airplaneLogBook = new AircraftLogBook();
					    airplaneLogBook.TaiNumber = aircraft.TailNumber.ToUpper();
					 }

					var contackBooks = db.ContactBooks.ToList();
					if (contackBooks != null && contackBooks.Count > 0)
						contactBook = contackBooks.Where(i => i.Name == "GENERAL").FirstOrDefault();

					if (contactBook == null)
					{
						contactBook = new ContactBook();
						contactBook.Name = "GENERAL";
					}

					
					
					var thisAssembly = Assembly.GetExecutingAssembly();
                    StreamReader fileNameSream = new StreamReader(thisAssembly.GetManifestResourceStream("ClubLogBook.Infrastructure.ClubLogbookDataToImport.DataResource.BazHaifaClubMembers.csv"));
                    var pilots = ConvertBuzClubMembers(db, fileNameSream, club,supportedClub,contactBook);
					
                    db.Clubs.UpdateRange(club);
					
					db.AircraftLogBooks.UpdateRange(airplaneLogBook);
                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return false;
            }
            return true;
        }
		public static bool S2_CreateClubGeneral(ApplicationDbContext db,string clubTailNumber, string clubName , SupportedClub supportedClub,string contactBookName = "GENERAL")
		{
			try
			{

				
				if (db != null)
				{
					List<Club> clubs = db.Clubs.ToList();
					AircraftLogBook airplaneLogBook = null;
					Aircraft aircraft = db.Aircrafts.Where(ac => ac.TailNumber.ToUpper() == clubTailNumber).SingleOrDefault();
					ContactBook contactBook = null;
					if (aircraft == null)
						return false;
					clubs = db.Clubs.ToList();
					Club club = null;
					if (clubs != null && clubs.Count > 0)
						club = clubs?.Where(x => x.Name.ToUpper() == clubName.ToUpper()).FirstOrDefault();
					if (club == null)
					{
						club = new Club();
						club.Name = clubName.ToUpper();

						club.AddAircraft(aircraft);
					}

					var airplaneLogBooks = db.AircraftLogBooks.ToList();
					if (airplaneLogBooks != null && airplaneLogBooks.Count > 0)
						airplaneLogBook = airplaneLogBooks.Where(x => x.TaiNumber.ToUpper() == aircraft.TailNumber.ToUpper()).Single();

					if (airplaneLogBook == null)
					{
						airplaneLogBook = new AircraftLogBook();
						airplaneLogBook.TaiNumber = aircraft.TailNumber.ToUpper();
					}
					var contackBooks = db.ContactBooks.ToList();
					if (contackBooks != null && contackBooks.Count > 0)
						contactBook = contackBooks.Where(i => i.Name.ToUpper() == contactBookName.ToUpper()).FirstOrDefault();

					if (contactBook == null)
					{
						contactBook = new ContactBook();
						contactBook.Name = contactBookName.ToUpper();
					}

					var thisAssembly = Assembly.GetExecutingAssembly();
					StreamReader fileNameSream = new StreamReader(thisAssembly.GetManifestResourceStream("ClubLogBook.Infrastructure.ClubLogbookDataToImport.DataResource.BazHaifaClubMembers.csv"));
					var pilots = ConvertBuzClubMembers(db, fileNameSream, club,supportedClub,contactBook);

					db.Clubs.UpdateRange(club);
					db.ContactBooks.UpdateRange(contactBook);
					db.AircraftLogBooks.UpdateRange(airplaneLogBook);
					db.SaveChanges();

				}
			}
			catch (Exception e)
			{
				Trace.WriteLine(e);
				return false;
			}
			return true;
		}

		public static bool S3_CreateLogBook(ApplicationDbContext db, string idNumber)
        {
            //using (db)
            try
            {
                LogBook lbNew = db.LogBooks.Where(x => x.Pilot.IdNumber == idNumber).SingleOrDefault();
                if (lbNew != null)
                    return true;
                List<Pilot> pl = db.Members.Where(x => x.IdNumber == idNumber).ToList();
                Member p = pl.FirstOrDefault();
                
                if (p != null)
                {
                    lbNew = new LogBook();
                    lbNew.Pilot = p as Pilot;
                    db.LogBooks.Add(lbNew);
                    db.SaveChanges();
                    return true;
                }

            }
            catch(Exception e)
            {
                Trace.WriteLine(e);
                return false;
            }
            return false;
        }
		
		
        public static bool S4_ConvertFromMyflightBookFlightCSV( ApplicationDbContext db, string idNumber)
        {
            try
            {

                var thisAssembly = Assembly.GetExecutingAssembly();
                StreamReader fileNameSream = new StreamReader(thisAssembly.GetManifestResourceStream("ClubLogBook.Infrastructure.ClubLogbookDataToImport.DataResource.MyFlightbook-Yosef.csv"));
                //using (db )
                {
                    db.LogBooks.Load();
                    List<LogBook> lb = db.LogBooks.ToList();
                    List<Aircraft> allAcAircraft = db.Aircrafts.ToList();
                    if (lb == null || allAcAircraft.Count() == 0)
                        return false;
                    LogBook currentLB = db.LogBooks.Where(p => p.Pilot.IdNumber == idNumber).SingleOrDefault();
                    if (currentLB == null)
                        return false;
                    if (currentLB.FlightRecords != null && currentLB.FlightRecords.Count() > 0)
                        return true;
                    List<CustomPropertyType> cptList = db.CustomPropertyTypes.ToList();
                    List<CustomPropertyType> cpt = cptList.Where(x => x.Title.Contains("Takeoffs - Night")).ToList();
                    string[] stringSeparators = new string[] { "\",\"" };

                    string source = fileNameSream.ReadLine();
                    string[] header = source.Split(stringSeparators, StringSplitOptions.None);
                    Dictionary<string, int> headerToIndex = new Dictionary<string, int>();
                    headerToIndex = header.Select((s, i) => new { i, s }).ToDictionary(x => x.s, x => x.i);
                    while (!fileNameSream.EndOfStream)
                    {
                        source = fileNameSream.ReadLine();
                        var record = source.Split(stringSeparators, StringSplitOptions.None);
                        string v;
                        short nShort;
                        int index;
                        headerToIndex.TryGetValue("Tail Number", out index);
                        v = record[index];
                        var ac = db.Aircrafts.Where(b => b.TailNumber.ToUpper() == v.ToUpper());
                        FlightRecord fr = new FlightRecord();
                        if (ac.FirstOrDefault() == null)
                        {
                            Trace.WriteLine($"Aircraf TailNumber {v}is Null");
                        }
                        fr.Aircraft = ac.FirstOrDefault();
                        headerToIndex.TryGetValue("Date", out index);
                        v = record[index];
                        v = v.Remove(0, 1);
                        //v= v.Replace('\"', Char.Parse(""));
                        fr.Date = Convert.ToDateTime(v);
                        headerToIndex.TryGetValue("Route", out index);
                        v = record[index];
                        fr.Routh = v;
                        headerToIndex.TryGetValue("CFI", out index);
                        v = record[index];
                        Decimal fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.CFI = fNumber;
                        headerToIndex.TryGetValue("Dual Received", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.Dual = fNumber;
                        headerToIndex.TryGetValue("PIC", out index);
                        v = record[index]; ;
                        fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.PIC = fNumber;

                        headerToIndex.TryGetValue("Total Flight Time", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.TotalTime = fNumber;

                        headerToIndex.TryGetValue("Approaches", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        nShort = Convert.ToInt16(v);
                        fr.Approaches = nShort;

                        headerToIndex.TryGetValue("Hold", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;

                        if (v.Length > 0)
                            nShort = (short)Convert.ToInt16(v);
                        fr.Holds = nShort;

                        headerToIndex.TryGetValue("Landings", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        nShort = Convert.ToInt16(v);
                        fr.Landings = nShort;

                        headerToIndex.TryGetValue("FS Night Landings", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        nShort = Convert.ToInt16(v);
                        fr.NightLandings = nShort;

                        headerToIndex.TryGetValue("X-Country", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.XCountry = fNumber;

                        headerToIndex.TryGetValue("Night", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.Night = fNumber;

                        headerToIndex.TryGetValue("IMC", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.Instrument = fNumber;

                        headerToIndex.TryGetValue("Simulated Instrument", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.Simulated = fNumber;

                        headerToIndex.TryGetValue("Ground Simulator", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        if (v.Length > 0)
                            fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.GroundSimulator = fNumber;

                        headerToIndex.TryGetValue("Comments", out index);
                        v = record[index];
                        fr.FlightComments = v;

                        headerToIndex.TryGetValue("Hobbs Start", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.HobbsStart = fNumber;

                        headerToIndex.TryGetValue("Hobbs End", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.HobbsEnd = fNumber;

                        headerToIndex.TryGetValue("Engine Start", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        if (v.Length > 0)
                            fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.EngineStart = fNumber;

                        headerToIndex.TryGetValue("Engine End", out index);
                        v = record[index];
                        v = v == "" ? "0" : v;
                        if (v.Length > 0)
                            fNumber = (Decimal)Convert.ToDecimal(v);
                        fr.EngineEnd = fNumber;
                        currentLB.Add(fr);
                        FlightProperty fp = new FlightProperty();
                        fp.CustomPropertyType = cpt.FirstOrDefault();
                        fp.FlightRecord = fr;
                        fp.DateValue = fr.Date;
                        fp.IntValue = 3;
                        //db.FlightProperties.Add(fp);
                        //db.FlightRecords.Add(fr);


                    }
                    db.LogBooks.Update(currentLB);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return false;
            }

            return true;
        }
		
		
        public static bool s5_ConverBuzClubAirplaneFlight(ApplicationDbContext db)
        {
            string[] row;
            List<Flight> flightsAddList = new List<Flight>();
            List<Flight> flightNotAddList = new List<Flight>();
            int addedCount = 0, addConflic = 0, addedEqual = 0, total = 0;
            try
            {
                var thisAssembly = Assembly.GetExecutingAssembly();
                StreamReader sr = new StreamReader(thisAssembly.GetManifestResourceStream("ClubLogBook.Infrastructure.ClubLogbookDataToImport.DataResource.4X-CGC_2018_flight.csv"));
                if (sr == null)
                    return false;
				//string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
				//	 "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
				//	 "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
				//	 "M/d/yyyy h:mm", "M/d/yyyy h:mm",
				//	 "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
				//	 "MM/d/yyyy HH:mm:ss.ffffff" };
				string[] formats = {"d/M/yyyy","dd/MM/yyyy", "dd/M/yyyy", "d/MM/yyyy" };

				{
                    //Pilot cpt = new Pilot();
                    Club clubBaz = db.Clubs.Where(x => x.Name.ToUpper() == "BAZ").SingleOrDefault();
                    if (clubBaz == null)
                        return false;
	                Aircraft clubAircraft = db.Aircrafts.Where(x => x.TailNumber.ToUpper() == "4xcgc".ToUpper())
		                .SingleOrDefault();
	                if (clubAircraft == null)
		                return false;
                    AircraftLogBook airplaneLogBook =
                        db.AircraftLogBooks.Where(x => x.TaiNumber.ToUpper() == clubAircraft.TailNumber.ToUpper()).SingleOrDefault();
                    if (airplaneLogBook == null)
                        return false;
                    List<Flight> flightToRemove = db.Flights.ToList();//airplaneLogBook.Flights;
                    db.Flights.RemoveRange(flightToRemove);
                    db.SaveChanges();
                    //flights = db.Flights.ToList();
                    //db.Pilots.Load();
                    string[] header = sr.ReadLine().Split(',');
                    while (!sr.EndOfStream)
                    {
                        Flight fl = new Flight();


                        row = sr.ReadLine().Split(',');

                        if (row.Length == 8)
                        {
                            total++;
                            Member pilot = db.Members.SingleOrDefault(x => x.IdNumber == row[1]);
                            if(pilot == null)
                            {
                                Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: Pilot is null in row :{string.Join(",",row)}");
                                continue;
                            }
                            
                            fl.Aircraft = clubAircraft;
	                        fl.Pilot = pilot as Pilot;
                            fl.Date = DateTime.ParseExact(row[2], formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
							fl.EngineStart = Convert.ToDecimal(row[6]);
                            fl.EngineEnd = Convert.ToDecimal(row[7]);
                            fl.HobbsStart = fl.EngineStart;
                            fl.HobbsEnd = fl.EngineEnd;
                            fl.Routh = "";

                            //Check If Flight Exist
                            if (flightsAddList.Where(x => x.Equals(fl)).Any())
                            {
                                Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: {fl.Date} Id:{fl.Aircraft.TailNumber} EngienS:{fl.EngineStart} EngienEnd:{fl.EngineEnd} Conflict");
                                if (flightsAddList.Where(x => x == fl).Any())
                                {
                                    addedEqual++;
                                    Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: {fl.Date} Id:{fl.Aircraft.TailNumber} EngienS:{fl.EngineStart} EngienEnd:{fl.EngineEnd} Equal To Other");
                                }
                                else
                                {
                                    addConflic++;
                                    Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: {fl.Date} Id:{fl.Aircraft.TailNumber} EngienS:{fl.EngineStart} EngienEnd:{fl.EngineEnd} Conflict");
                                }
                                flightNotAddList.Add(fl);
                            }
                            else
                            {
                                flightsAddList.Add(fl);
                                addedCount++;
                                Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: {fl.Date} Id:{fl.Aircraft.TailNumber} EngienS:{fl.EngineStart} EngienEnd:{fl.EngineEnd} CanBe Add");
                            }

                            airplaneLogBook.Add(fl);
                            //db.AircraftLogBooks.Update(airplaneLogBook);
                            //db.SaveChanges();
                        }
                        else
                        {
                            Trace.WriteLine("Error Num Of Field");
                        }
                    }
                    db.AircraftLogBooks.UpdateRange(airplaneLogBook);
                    db.SaveChanges();
                }
                Trace.WriteLine($"ConverBuzClubAirplaneFlight:Total:{total} added:{addedCount} Conflict:{addConflic} Equal:{addedEqual}");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return false;

            }
            return true;
        }
		
		
        public static List<AirplanemodelImportcs> LoadJson()
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            using (StreamReader r = new StreamReader(thisAssembly.GetManifestResourceStream("ClubLogBook.Infrastructure.ClubLogbookDataToImport.DataResource.AirplaneModel.json")))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<AirplanemodelImportcs>>(json);
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].AircraftID = 0;
                }
                return items;
            }
        }

        private static List<Pilot> ConvertBuzClubMembers(ApplicationDbContext db, StreamReader sr, Club club ,SupportedClub supportedClub ,ContactBook contactBook)
        {

            List<Pilot> cptList = new List<Pilot>();
			List<Contact> contactList = new List<Contact>();
			
			string[] row;
            try
            {
                byte[] defaultFemalePhoto = GetDefaultPilotPhoto(Gender.Female);
                byte[] defaultMalePhoto = GetDefaultPilotPhoto(Gender.Male);
                //using (db )
                {
                    if (sr == null)
                        return cptList;
                    db.Members.Load();
                    cptList = db.Members.ToList();
                    string[] header = sr.ReadLine().Split(',');
					
					
					while (!sr.EndOfStream)
                    {
                        Pilot cpt = new Pilot();
                        
						Contact contact = new Contact();
                        row = sr.ReadLine().Split(',');

                        if (row.Length == 9)
                        {
                            cpt.IdNumber = row[0];

                            cpt.FirstName = row[1];
                            cpt.LastName = row[2];
                            cpt.MiddleName = "";
                            cpt.Gender = Convert.ToInt32(row[7]) == 0 ? Gender.Male : Gender.Female;
                            cpt.Photo = cpt.Gender == Gender.Male ? defaultMalePhoto : defaultFemalePhoto;
                            cpt.DateOfBirth = DateTime.Now;
							EMAIL eMAIL = new EMAIL() { EMail = row[3] };
							Phone phone = new Phone() { PhoneNumber = row[4], Type = ContactType.HOME };
							contact.EMAILs.Add(eMAIL);
							contact.Phones.Add(phone);
							Address address = new Address();
							address.Street = row[6];
                            contact.Addresses.Add( address);
							//contactBook.AddContact(contact);
							//db.Addresses.Update(address);
							//db.EMAILs.Update(eMAIL);
							//db.Phones.Update(phone);
							contactList.Add(contact);
							cpt.Contact = contact;
                            if (club != null)
                            {

								//if (Convert.ToInt32(row[8]) == 0 || (Convert.ToInt32(row[8]) == 11))
								SupportedClub current = (SupportedClub)(Convert.ToInt32(row[8]));
								if (current.HasFlag(supportedClub))
								{

                                    
                                    club.AddMember(cpt);
                                }
                            }

                            if (cptList.Count == 0 || cptList.Where(x => x.IdNumber == cpt.IdNumber).SingleOrDefault() == null)
                                cptList.Add(cpt);

                        }
                        else
                        {
                            Trace.WriteLine("Error Num Of Field");
                        }
						contactBook.Contacts.Add(contact);
						//db.Contacts.Add(contact);
						//db.SaveChanges();
						
					}
					
					db.ContactBooks.UpdateRange(contactBook);
                   db.Members.UpdateRange(cptList);
					
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

            }
            return cptList;
        }

        /*		

                //[Test]
                public void CreateFull()
                {
                    try
                    {
                        FullContex fullContex = new FullContex(FullContex.GetLogBookDbName());
                        List<Aircraft> list = fullContex.Aircraft.ToList();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }
                }
                //[Test]
                public void TestConvertFromMyflightBookFlight()
                {
                    ConvertFromMyflightBookFlightJason(059828392);
                }
                public bool ConvertFromMyflightBookFlightJason(int idNumber)
                {
                    try
                    {
                        List<Dictionary<string, string>> flightRecordsList = LoadFlightRecordsJson();

                        using (var db = new FullContex(FullContex.GetLogBookDbName()))
                        {
                            List<LogBook> lb = db.LogBooks.ToList();
                            List<Aircraft> allAcAircraft = db.Aircraft.ToList();
                            if (lb == null)
                                return false;
                            var currentLB = lb.Where(x => x.Pilot.IdNumber == idNumber);
                            List<CustomPropertyType> cptList = db.CustomPropertyTypes.ToList();
                            List<CustomPropertyType> cpt = cptList.Where(x => x.Title.Contains("Takeoffs - Night")).ToList();
                            foreach (var record in flightRecordsList)
                            {
                                string v;
                                short nShort;
                                record.TryGetValue("TailNumber", out v);
                                var ac = db.Aircraft.Where(b => b.TailNumber.ToUpper() == v.ToUpper());
                                FlightRecord fr = new FlightRecord();
                                if (ac.FirstOrDefault() == null)
                                {
                                    Trace.WriteLine($"Aircraf TailNumber {v}is Null");
                                }
                                fr.Aircraft = ac.FirstOrDefault();
                                fr.Routh = "";
                                record.TryGetValue("Date", out v);
                                DateTime dt = Convert.ToDateTime(v);
                                fr.Date = dt.Date;
                                record.TryGetValue("CFI", out v);
                                Decimal fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.CFI = fNumber;
                                record.TryGetValue("Dual Received", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.Dual = fNumber;
                                record.TryGetValue("PIC", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.PIC = fNumber;

                                record.TryGetValue("Total Flight Time", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.TotalTime = fNumber;

                                record.TryGetValue("Solo Time", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.Solo = fNumber;

                                record.TryGetValue("Approaches", out v);
                                nShort = Convert.ToInt16(v);
                                fr.Approaches = nShort;

                                record.TryGetValue("Hold", out v);
                                if (v.Length > 0)
                                    nShort = (short)Convert.ToInt16(v);
                                fr.Holds = nShort;

                                record.TryGetValue("Landings", out v);
                                nShort = Convert.ToInt16(v);
                                fr.Landings = nShort;

                                record.TryGetValue("FS Night Landings", out v);
                                nShort = Convert.ToInt16(v);
                                fr.NightLandings = nShort;

                                record.TryGetValue("X-Country", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.XCountry = fNumber;

                                record.TryGetValue("Night", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.Night = fNumber;

                                record.TryGetValue("IMC", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.Instrument = fNumber;

                                record.TryGetValue("Simulated Instrument", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.Simulated = fNumber;

                                record.TryGetValue("Ground Simulator", out v);
                                if (v.Length > 0)
                                    fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.GroundSimulator = fNumber;

                                record.TryGetValue("Comments", out v);
                                fr.FlightComments = v;

                                record.TryGetValue("Hobbs Start", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.HobbsStart = fNumber;

                                record.TryGetValue("Hobbs End", out v);
                                fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.HobbsEnd = fNumber;

                                record.TryGetValue("Engine Start", out v);
                                if (v.Length > 0)
                                    fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.EngineStart = fNumber;

                                record.TryGetValue("Engine End", out v);
                                if (v.Length > 0)
                                    fNumber = (Decimal)Convert.ToDecimal(v);
                                fr.EngineEnd = fNumber;
                                currentLB.FirstOrDefault().FlightRecords.Add(fr);
                                FlightProperty fp = new FlightProperty();
                                fp.CustomPropertyType = cpt.FirstOrDefault();
                                fp.FlightRecord = fr;
                                fp.DateValue = fr.Date;
                                fp.IntValue = 3;
                                db.FlightProperties.UpdateRange(fp);

                                db.SaveChanges();
                            }


                        }
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e);
                        return false;
                    }

                    return true;
                }
                //[Test]
                public void TestConvertFromMyflightBookFlightCSV()
                {
                    try
                    {
                        ConvertFromMyflightBookFlightCSV(059828392);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }
                }



                public static List<Dictionary<string, string>> LoadFlightRecordsJson()
                {
                    List<Dictionary<string, string>> FlightRecord = new List<Dictionary<string, string>>();
                    JArray o1 = JArray.Parse(File.ReadAllText(@"C:\Users\Administrator\Documents\Visual Studio 2017\Projects\AsyncDemo\AsyncDemo\AirplaneModel\FlightRecords.json"));

                    // read JSON directly from a file
                    using (StreamReader file = File.OpenText(@"C:\Users\Administrator\Documents\Visual Studio 2017\Projects\AsyncDemo\AsyncDemo\AirplaneModel\FlightRecords.json"))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JArray o2 = (JArray)JToken.ReadFrom(reader);
                        foreach (var record in o2)
                        {
                            Dictionary<string, string> lst = record.ToObject<Dictionary<string, string>>();
                            FlightRecord.Add(lst);

                        }

                    }
                    return FlightRecord;
                }
                //[Test]
                public void AddPilot(int idNumber)
                {
                    using (var db = new FullContex(FullContex.GetLogBookDbName()))
                    {
                        db.Pilots.Load();
                        List<Pilot> pilot = db.Pilots.ToList();
                        List<Pilot> p = pilot.FindAll(x => x.IdNumber == idNumber);
                        if (p.Any() == false)
                        {
                            Pilot pNew = new Pilot(idNumber);

                            pNew.DateOfBirth = new DateTime(1965, 08, 21);
                            pNew.Gender = Gender.Male;
                            pNew.FirstName = "Yosef";
                            pNew.Height = 172;
                            pNew.LastName = "Levy";
                            Address address = new Address();
                            address.Address1 = "Internanl Box 248";
                            address.City = "gilon";
                            address.Mail = "yos.1965@gmail.com";
                            address.Phone = "0549050740";
                            db.Addresses.Add(address);
                            pNew.HomeAddress = address;
                            db.Pilots.Add(pNew);
                            db.SaveChanges();
                        }
                        else
                        {
                            Address address = new Address();
                            address.Address1 = "Internanl Box 248";
                            address.City = "Eilat";
                            address.Mail = "yos.1965@gmail.com";
                            address.Phone = "0549050740";
                            db.Addresses.Add(address);
                            p.FirstOrDefault().HomeAddress = address;
                            db.SaveChanges();
                        }
                    }
                }

            //	[Test]
                public void AddFligthToLoogBook()
                {
                    using (var db = new FullContex(FullContex.GetLogBookDbName()))
                    {
                        //db.FlightRecords.Load();
                        List<FlightRecord> fr = db.FlightRecords.ToList();
                        if (fr.Any())
                        {
                            List<LogBook> lb = db.LogBooks.ToList();
                            if (lb.Any())
                            {
                                lb.Clear();
                                db.SaveChanges();
                            }
                            LogBook lbNew = new LogBook();
                            List<Pilot> pl = db.Pilots.Where(x => x.IdNumber == 059828392).ToList();
                            Pilot p = pl.FirstOrDefault();
                            lbNew.Pilot = p;
                            //foreach (var f in fr)
                            //{
                            //	lbNew.FlightRecords.Add(f);
                            //}
                            lbNew.FlightRecords.AddRange(fr);
                            db.LogBooks.Add(lbNew);





                            db.SaveChanges();
                        }
                    }
                }
                //[Test]

            //	[Test]
                public void TestConvertCustomPropertyTypes()
                {
                    var thisAssembly = Assembly.GetExecutingAssembly();
                    //var myFileData = thisAssembly.GetFile("LogBookDataAccess.AirplaneModel.custompropertytypes.csv");
                    StreamReader fileNameSream = new StreamReader(thisAssembly.GetManifestResourceStream("FullLogBook.NetCore.DataResource.custompropertytypes.csv"));
                    List<CustomPropertyType> cptList = ConvertCustomPropertyTypes(fileNameSream);

                    //cptList.FirstOrDefault().DumpAll();

                }



                //[Test]
                public void TestConvertBuzClubMembers()
                {
                    try
                    {

                        var thisAssembly = Assembly.GetExecutingAssembly();
                        StreamReader fileNameSream = new StreamReader(thisAssembly.GetManifestResourceStream("FullLogBook.NetCore.DataResource.BazHaifaClubMembers.csv"));
                        var pilots = ConvertBuzClubMembers(fileNameSream, null);
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e);
                    }
                }

                //[Test]
                public void TestConverBuzClubAirplaneFlight()
                {
                    ConverBuzClubAirplaneFlight();
                }


                //[Test]
                public void TestConverBuzClubAirplaneFlightExcept()
                {
                    ConverBuzClubAirplaneFlightExcept();
                }
                public bool ConverBuzClubAirplaneFlightExcept()
                {
                    string[] row;

                    List<Flight> flightsToAdd = new List<Flight>();
                    int addedCount = 0, addConflic = 0, addedEqual = 0, total = 0;
                    try
                    {
                        var thisAssembly = Assembly.GetExecutingAssembly();
                        StreamReader sr = new StreamReader(thisAssembly.GetManifestResourceStream("FullLogBook.NetCore.DataResource.4X-CGC_2018_flight.csv"));
                        if (sr == null)
                            return false;
                        string[] header = sr.ReadLine().Split(',');
                        while (!sr.EndOfStream)
                        {
                            Flight fl = new Flight();


                            row = sr.ReadLine().Split(',');

                            if (row.Length == 6)
                            {
                                total++;
                                fl.IdNumber = Convert.ToInt32(row[1]);
                                fl.Date = Convert.ToDateTime(row[2]);
                                fl.EngineStart = Convert.ToDecimal(row[4]);
                                fl.EngineEnd = Convert.ToDecimal(row[5]);
                                fl.HobbsStart = fl.EngineStart;
                                fl.HobbsEnd = fl.EngineEnd;
                                flightsToAdd.Add(fl);
                                Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: {fl.Date} Id:{fl.IdNumber} EngienS:{fl.EngineStart} EngienEnd:{fl.EngineEnd}  Add To List");
                                //db.Flights.UpdateRange(fl);
                            }
                            else
                            {
                                Trace.WriteLine("Error Num Of Field");
                            }
                        }
                        var db = new FullContex(FullContex.GetLogBookDbName());

                        //Pilot cpt = new Pilot();
                        List<Flight> flights = new List<Flight>();
                        //; = db.Flights.ToList();
                        foreach (var conflic in db.Flights.ToList())
                        {
                            flights.Add(conflic);
                            Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: {conflic.Date} Id:{conflic.IdNumber} EngienS:{conflic.EngineStart} EngienEnd:{conflic.EngineEnd}  Add To List");
                        }
                        List<Flight> inDB = flights.Intersect(flightsToAdd).ToList();
                        foreach (var conflic in inDB)
                        {
                            Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: {conflic.Date} Id:{conflic.IdNumber} EngienS:{conflic.EngineStart} EngienEnd:{conflic.EngineEnd}  Add To List");
                        }
                        List<Flight> newFlights = flightsToAdd.Except(flights, new ObjectExtensions.FlightComparer()).ToList();
                        foreach (var conflic in newFlights)
                        {
                            Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: {conflic.Date} Id:{conflic.IdNumber} EngienS:{conflic.EngineStart} EngienEnd:{conflic.EngineEnd}  Add To List");
                        }
                        List<Flight> conflicFlights = newFlights.Intersect(flightsToAdd, new ObjectExtensions.FlightComparer()).ToList();
                        //db.Pilots.Load();
                        foreach (var conflic in conflicFlights)
                        {
                            Trace.WriteLine($"ConverBuzClubAirplaneFlight: Flight: {conflic.Date} Id:{conflic.IdNumber} EngienS:{conflic.EngineStart} EngienEnd:{conflic.EngineEnd}  Add To List");
                        }

                        //db.SaveChanges();

                        Trace.WriteLine($"ConverBuzClubAirplaneFlight:Total:{total} added:{addedCount} Conflict:{addConflic} Equal:{addedEqual}");
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e);
                        return false;

                    }
                    return true;
                }
				*/
                public static bool UpdatePilotDefaultPhoto(ApplicationDbContext db)
                {
                    try
                    {
                        
                        List<Pilot> pilots = db.Members.ToList();
                        foreach (var p in pilots)
                        {
							if(p.Photo == null)
								p.Photo = GetDefaultPilotPhoto(p.Gender);
                        }
                        db.UpdateRange(pilots);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e);
                        return false;
                    }
                    return true;
                }

                public static bool UpdateairplaneDefaultPhoto(ApplicationDbContext db)
                {
                    try
                    {
                        
                        List<Aircraft> aircrafts = db.Aircrafts.ToList();
                        foreach (var p in aircrafts)
                        {
	                        if (p.Photo == null)
	                        {
								p.Photo = GetDefaultAirplanePhoto();
							}
                            
                        }
                        db.UpdateRange(aircrafts);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e);
                        return false;
                    }
                    return true;
                }
                
    }
}
