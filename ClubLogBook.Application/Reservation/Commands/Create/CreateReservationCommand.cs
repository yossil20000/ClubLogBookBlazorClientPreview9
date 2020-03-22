using System.Linq;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ClubLogBook.Application.Accounts.Queries.GetAccountsList;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ClubLogBook.Application.Models;
using ClubLogBook.Core.Interfaces;
using System;
using ClubLogBook.Application.Extenttions;
using ClubLogBook.Application.Common.Models;

namespace ClubLogBook.Application.Reservation.Queries
{
	public class CreateReservationCommand : IRequest<Result>
	{
		public FlightReservationCreateModel FlightReservationCreateModel { get; set; }
		public CreateReservationCommand(FlightReservationCreateModel flightReservationCreateModel)
		{
			FlightReservationCreateModel = flightReservationCreateModel;
		}
	}
	public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Result>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public CreateReservationCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Result> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
		{
			Result result = new Result() ;
			AircraftReservation aircraftReservation = new AircraftReservation();
			var aircraft = _context.Set<Aircraft>().Find(request.FlightReservationCreateModel.AircraftId);
			if (aircraft != null)
				aircraftReservation.AircraftId = aircraft.Id;
			
			
			aircraftReservation.AircraftId = request.FlightReservationCreateModel.AircraftId;
			var pilot = _context.Set<Pilot>().Find(request.FlightReservationCreateModel.PilotId);
			if (pilot == null)
			{
				result.AddError($"Pilot With Id:{request.FlightReservationCreateModel.PilotId} Not Found");
			}
			aircraftReservation.PilotId = pilot.Id;
			request.FlightReservationCreateModel.CombineTime();
			aircraftReservation.IdNumber = pilot.IdNumber;
			aircraftReservation.TailNumber = aircraft.TailNumber;
			aircraftReservation.DateFrom = request.FlightReservationCreateModel.DateFrom;
			aircraftReservation.DateTo = request.FlightReservationCreateModel.DateTo;
			var reservations = await _context.Set<AircraftReservation>().ToListAsync();
			if(reservations.IsFlightReservationValid(aircraftReservation))
			{
				var reservation = _context.Set<AircraftReservation>().Update(aircraftReservation);
				if (reservation != null)
				{
					int rVal = await _context.SaveChangesAsync(cancellationToken);
					if( rVal ==0 )
					{
						result.AddError($"CreateReservationCommand Save Failed");
					}
				}
			}
			else
			{
				result.AddError($"Reservation Already Exist Date Conflict");
			}
			
			return result;
		}
	}

	public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
	{
		public CreateReservationCommandValidator()
		{
			RuleFor(x => x.FlightReservationCreateModel.AircraftId).GreaterThan(0).WithMessage("AircraftId Must Be Grater Then 0");
			RuleFor(x => x.FlightReservationCreateModel.PilotId).GreaterThan(0).WithMessage("PilotId Must Be Grater Then 0");
		}
	}
}
