using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.ViewModels;

namespace ClubLogBook.Application.AircraftManager.Commands
{
	public class CreateAircraftCommand : IRequest<int>
	{
		public AircraftViewModel aircraftViewModel { get; set; } = new AircraftViewModel();
		public CreateAircraftCommand()
		{
		}
		
	}
}
