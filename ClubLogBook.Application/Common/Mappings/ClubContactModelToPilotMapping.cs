using AutoMapper;
using ClubLogBook.Application.Models;
using ClubLogBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Application.Common.Mappings
{
	public static class ClubContactModelToPilotMapping
	{
		public static Pilot ClubContactsModelToPilot(IMapper mapper, ClubContactsModel clubContactsModel)
		{
			Pilot pilot = mapper.Map<ClubContactsModel, Pilot>(clubContactsModel);
			return pilot;
		}
		public static ClubContactsModel PilotToClubContactsModel(IMapper mapper, Pilot pilot)
		{
			ClubContactsModel clubContact = new ClubContactsModel();
			if (pilot != null)
			{
				if (pilot.UserId == null) pilot.UserId = string.Empty;

				mapper.Map<Pilot, ClubContactsModel>(pilot, clubContact);
				//ccVM.CopyPilot(p);
				clubContact.Id = pilot.Id; clubContact.IdNumber = pilot.IdNumber; clubContact.FullName = $"{pilot.FirstName} {pilot.LastName}";
				clubContact.Gender = (Gender)pilot.Gender;
				clubContact.DateOfBirth = pilot.DateOfBirth == null ? DateTime.Now : pilot.DateOfBirth;
				clubContact.UserId = pilot.UserId;
				if (pilot.Contact != null)
				{
					mapper.Map<ICollection<Address>, List<AddressModel>>(pilot.Contact.Addresses, clubContact.Addresses);
					mapper.Map<ICollection<Phone>, List<PhoneModel>>(pilot.Contact.Phones, clubContact.Phones);
					mapper.Map<ICollection<EMAIL>, List<EMAILModel>>(pilot.Contact.EMAILs, clubContact.Emails);

				}

				if (clubContact.Phones.Count == 0)
				{
					clubContact.Phones.Add(new PhoneModel());
				}
				if (clubContact.Emails.Count == 0)
				{
					clubContact.Emails.Add(new EMAILModel());
				}
				if (clubContact.Addresses.Count == 0)
				{
					clubContact.Addresses.Add(new AddressModel());
				}

			}
			return clubContact;
		}
	}
}
