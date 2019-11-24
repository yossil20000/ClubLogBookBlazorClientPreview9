using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces.Mapping;
using ClubLogBook.Application.ViewModels;
namespace ClubLogBook.Application.AircraftManager.Queries
{
	public class AircraftLookupModel : AircraftViewModel, IHaveCustomMapping
	{
		public AircraftLookupModel()
		{
		}

		public AircraftLookupModel(Aircraft aircraft) : base(aircraft)
		{
		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<ClubLogBook.Core.Entities.Aircraft, AircraftLookupModel>();
		}
	}
}
