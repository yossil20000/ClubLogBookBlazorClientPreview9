using System;
using NUnit.Framework;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Infrastructure.Data;
using ClubLogBook.Infrastructure.Persistence;
using UnitsTest.ApplicationInfrastructure;

using System.Threading.Tasks;
using ClubLogBook.Application.Models;
using ClubLogBook.Application.Services;
using ClubLogBook.Server.Services;
using AutoMapper;
using ContactType = ClubLogBook.Core.Interfaces.ContactType;

namespace UnitsTest.AplicationServer
{
	public class ServerServiceTest
	{
		public ClubRepository cr;
		ApplicationDbContext _context;

		IMapper autoMapper;
		[Test]
		public  async Task ClubContactsViewModelServiceTest()
		{
			autoMapper = AutoMapperConstructor.Instance.Mapper;
			ImportDataTest import = new ImportDataTest();
			import.InitContext();
			try
			{
				_context = import._context;
				MemberRepository mr = new MemberRepository(_context);
				MemberService ms = new MemberService(mr);
				FlightRepository fr = new FlightRepository(_context);
				
				ClubRepository cr = new ClubRepository(_context);

				ClubService clubService = new ClubService(cr, mr, fr, null, null, null);
				//ICollection<Pilot> clubBaz = await clubService.GetClubMembers("BAZ");

				autoMapper = AutoMapperConstructor.Instance.Mapper;

				ClubContactsViewModelService clubContactsViewModelService = new ClubContactsViewModelService(clubService,ms,autoMapper);
				//IEnumerable<ClubContactsViewModel> clubContacs = await  clubContactsViewModelService.GetOrCreateClubContact("Baz");
				//cl.Members = members;
				//var p = clubContacs.Where(i => i.IdNumber == "059828392").FirstOrDefault();
				ClubContactsModel clubContactUpdate = new ClubContactsModel();
				Contact contact = new Contact();
				contact.Addresses.Add(new Address() { City = "Gilon", Country = "Israel", State = "IL", Zipcode = "2010300", Street = "Ofir 60", Type = ContactType.HOME });
				
				contact.EMAILs.Add(new EMAIL() { EMail = "yonyon@gmail.com", Type = ContactType.WORK });
				contact.Phones.Add(new Phone() { PhoneNumber = "05490777553", Type = ContactType.HOME });
				clubContactUpdate.DateOfBirth = DateTime.Now;
				
				//var members = await clubService.GetClubMembers("BAZ");
				await clubContactsViewModelService.UpdateOrCreateClubContactMember("BAZ", clubContactUpdate);
				
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			return ;
		}
	}
}
