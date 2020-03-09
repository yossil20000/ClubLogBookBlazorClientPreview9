using AutoMapper;
using ClubLogBook.Application.Interfaces.Mapping;
using ClubLogBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Application.Models
{
	public class ClubModel : IHaveCustomMapping
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Club, ClubModel>();
		}
	}
}
