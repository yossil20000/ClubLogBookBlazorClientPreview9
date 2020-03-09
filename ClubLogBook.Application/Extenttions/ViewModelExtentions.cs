using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Models;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Application.Extenttions
{
	public static class ViewModelExtentions
	{
		public static void MarkNonValidFlight(this List<ClubFlightModel> FlightRecords)
		{
			var order = FlightRecords.OrderBy(x => x.EngineStart).ThenBy(x => x.EngineEnd).ToList();
			for (int i = 0; i < order.Count - 1; i++)
			{
				if (order[i].EngineEnd != order[i + 1].EngineStart)
				{
					order[i].IsValid = false;
					order[i+1].IsValid = false;
				}
			}
		}
		//public static void CopyPilot(this ClubContactsViewModel obj,PilotSelectViewModel pilot)
		//{
		//	obj.DateOfBirth = pilot.DateOfBirth;

		//	obj.Gender = pilot.Gender;
		//	obj.FirstName = pilot.FirstName;
		//	obj.LastName = pilot.LastName;
		//	obj.MiddleName = pilot.MiddleName;
		//	obj.Id = pilot.Id;
		//	obj.IdNumber = pilot.IdNumber;
		//	obj.Height = pilot.Height;
		//	obj.Weight = pilot.Weight;
		//	obj.UserId = pilot.UserId;
		//	//if (pilot.Contact?.Addresses?.Where(x => x.Type == ContactType.HOME).FirstOrDefault() != null)
		//	//	this.Address = pilot.Contact?.Addresses?.FirstOrDefault().ToString();
		//	//if (pilot.Contact?.Phones?.FirstOrDefault() != null)
		//	//	this.Phone = pilot.Contact?.Phones?.Where(x => x.Type == ContactType.HOME).FirstOrDefault().PhoneNumber.ToString();
		//	//if (pilot.Contact?.EMAILs?.FirstOrDefault() != null)
		//	//	this.Mail = pilot.Contact?.EMAILs?.Where(x => x.Type == ContactType.HOME).FirstOrDefault().EMail.ToString();
		//	//this.Licenses = pilot.Licenses;
		//	//Contacts.SetPhones(pilot.Contact.Phones);
		//	//Phones = pilot.Contact.Phones.ToList();

		//	//Emails = pilot.Contact.EMAILs.ToList();

		//}
	}
}
