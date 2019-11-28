using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace ClubLogBook.Application.AircraftManager.Commands.CreateAircraft
{
	public class CreateAircraftCommandValidator :AbstractValidator<CreateAircraftCommand>
	{
		public CreateAircraftCommandValidator()
		{
			
			RuleFor(x => x.aircraftViewModel.Id).Equals(0);
			RuleFor(x => x.aircraftViewModel.TailNumber).NotEmpty();
		}
	}
}
