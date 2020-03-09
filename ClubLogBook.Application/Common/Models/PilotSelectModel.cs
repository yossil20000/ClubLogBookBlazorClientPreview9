using AutoMapper;
using ClubLogBook.Application.Interfaces.Mapping;
using ClubLogBook.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
namespace ClubLogBook.Application.Models
{
	public class PilotSelectModel : IHaveCustomMapping
	{
		public int Id { get; set; }
		[Display(Name = "Given Name")]
		public string FirstName { get; set; }
		[Display(Name = "Family Name")]
		public string LastName { get; set; }
		[Display(Name ="Id Number")]
		public string IdNumber { get; set; }
		[Display(Name = "Name")]
		public string FullName { get { return $"{FirstName} {LastName}"; } }
		public bool IsEnable { get; set; } = true;
		public string UserId { get; set; }
		
		public PilotSelectModel()
		{ }
		public PilotSelectModel(Pilot p)
		{
			SetPilot(p);
		}
		private void SetPilot(Pilot p)
		{
			Id = p.Id;
			FirstName = p.FirstName;
			LastName = p.LastName;
			IdNumber = p.IdNumber;
			UserId = p.UserId;
		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Pilot, PilotSelectModel>();
			
		}
	}

}
