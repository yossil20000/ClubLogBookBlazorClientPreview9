﻿using System.Linq;
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
namespace ClubLogBook.Application.Reservation.Queries
{
	public class UpdateReservationCommand : IRequest<int>
	{
		public FlightReservationModel FlightReservationModel { get; set; }
		public UpdateReservationCommand(FlightReservationModel flightReservationModel)
		{
			FlightReservationModel = flightReservationModel;
		}
	}
	public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, int>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public UpdateReservationCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<int> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
		{
			int result = 0;	
			request.FlightReservationModel.CombineTime();
			AircraftReservation aircraftReservation = _mapper.Map<FlightReservationModel, AircraftReservation>(request.FlightReservationModel);
			var pilot = _context.Set<Pilot>().Find(aircraftReservation.PilotId);
			if (pilot == null)
				return 0;
			aircraftReservation.ReservationInfo = new UserInfo(pilot).GetJason();
			var reservation = _context.Set<AircraftReservation>().Update(aircraftReservation);
			if (reservation != null)
			{
				result = await _context.SaveChangesAsync(cancellationToken);
			}
			return result;
		}
	}

	public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
	{
		public UpdateReservationCommandValidator()
		{
			RuleFor(x => x.FlightReservationModel.Id).GreaterThan(0).WithMessage("ReservationId Must Be Greter Then 0");
		}
	}
}
