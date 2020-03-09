using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.Models;

namespace ClubLogBook.Application.AircraftManager.Commands
{
	public class CreateAircraftCommand : IRequest<int>
	{
		public AircraftModel aircraftViewModel { get; set; } = new AircraftModel();
		public CreateAircraftCommand()
		{
		}
		
	}
}
