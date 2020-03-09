using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using AutoMapper;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Models;
namespace ClubLogBook.Application.Infrastructure.AutoMapper
{
	public class AutoMapperProfile:Profile
	{
		public AutoMapperProfile()
		{
			LoadStandardMappings();
			LoadCustomMappings();
			LoadConverters();
			MappingProfile();
		}
		public void MappingProfile()
		{

			CreateMap<Flight, ClubFlightModel>().PreserveReferences().MaxDepth(4);
			CreateMap<ClubFlightModel, Flight>().PreserveReferences().MaxDepth(4);
			CreateMap<Aircraft, Aircraft>();
			CreateMap<AirCraftModel, AirCraftModel>();
			CreateMap<Pilot, Pilot>();
			CreateMap<Contact, Contact>();
			CreateMap<Address, Address>();
			CreateMap<Phone, Phone>();
			CreateMap<EMAIL, EMAIL>();
			CreateMap<Endorsment, Endorsment>();
			CreateMap<License, License>();
			CreateMap<Checkride, Checkride>();
			CreateMap<Aircraft, AirplaneSelectModel>();
			CreateMap<AircraftReservation, FlightReservationModel>().ReverseMap();
			//CreateMap<ApplicationUser, AdminUserInfo>();
			//CreateMap<AdminUserInfo, ApplicationUser>();
			CreateMap<Address, AddressModel>();
			CreateMap<Phone, PhoneModel>();
			CreateMap<EMAIL, EMAILModel>();
			CreateMap<ClubContactsModel, Pilot>();
			CreateMap<Pilot, ClubContactsModel>();
			CreateMap<AddressModel, Address>();
			CreateMap<PhoneModel, Phone>();
			CreateMap<EMAILModel, EMAIL>();
			CreateMap<Account, AccountModel>().PreserveReferences().MaxDepth(4);
			CreateMap<Invoice, InvoiceModel>().PreserveReferences().MaxDepth(4); ;
			CreateMap<AircraftPrice, AircraftPriceModel>().PreserveReferences().MaxDepth(4);
			CreateMap<Transaction, TransactionModel>().PreserveReferences().MaxDepth(4);
			//CreateMap<List<Transaction>, List<TransactionViewModel>>();
			CreateMap<Aircraft, AircraftModel>();
			CreateMap<AirCraftModel, AirCraftModelModel>().ReverseMap();
			CreateMap<AircraftClass, AircraftClassModel>().ReverseMap();
			CreateMap<AircraftCategory, AircraftCategoryModel>().ReverseMap();
			CreateMap<AircraftState, AircraftStateModel>().ReverseMap();

		}
		private void LoadConverters()
		{

		}
		private void LoadStandardMappings()
		{
			var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());
			foreach(var map in mapsFrom)
			{
				CreateMap(map.Source, map.Destination).ReverseMap();
			}
		}

		private void LoadCustomMappings()
		{
			var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());
			foreach(var map in mapsFrom)
			{
				map.CreateMappings(this);
			}
		}
	}
}
