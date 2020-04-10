using System.Linq;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ClubLogBook.Application.Models;
using ClubLogBook.Application.Extenttions;
using ClubLogBook.Application.Common.Models;

namespace ClubLogBook.Application.Reservation.Queries
{
	public class UpdateReservationCommand : IRequest<Result>
	{
		public FlightReservationModel FlightReservationModel { get; set; }
		public UpdateReservationCommand(FlightReservationModel flightReservationModel)
		{
			FlightReservationModel = flightReservationModel;
		}
	}
	public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Result>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public UpdateReservationCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<Result> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
		{
			List<string> errorList = new List<string>();
			AircraftReservation aircraftReservation = new AircraftReservation();
			var aircraft = _context.Set<Aircraft>().Find(request.FlightReservationModel.AircraftId);
			if (aircraft != null)
				aircraftReservation.AircraftId = aircraft.Id;


			aircraftReservation.AircraftId = request.FlightReservationModel.AircraftId;
			var pilot = _context.Set<Pilot>().Find(request.FlightReservationModel.PilotId);
			if (pilot == null)
			{
				errorList.Add($"Pilot With Id:{request.FlightReservationModel.PilotId} Not Found");
			}
			aircraftReservation.PilotId = pilot.Id;
			//request.FlightReservationModel.CombineTime();
			aircraftReservation.IdNumber = pilot.IdNumber;
			aircraftReservation.TailNumber = aircraft.TailNumber;
			aircraftReservation.DateFrom = request.FlightReservationModel.DateFrom;
			aircraftReservation.DateTo = request.FlightReservationModel.DateTo;
			aircraftReservation.Id = request.FlightReservationModel.Id;
			var reservations =  _context.Set<AircraftReservation>().AsNoTracking().ToList();
			if (reservations.IsFlightReservationValid(aircraftReservation))
			{
				var reservation = _context.Set<AircraftReservation>().Update(aircraftReservation);
				if (reservation != null)
				{
					int rVal = await _context.SaveChangesAsync(cancellationToken);
					if (rVal == 0)
					{
						errorList.Add($"CreateReservationCommand Save Failed");
					}
				}
			}
			else
			{
				errorList.Add($"Reservation Already Exist Date Conflict");
			}

			return (errorList.Count > 0) ? Result.Failure(errorList) : Result.Success();
		}
		//public async Task<int> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
		//{
		//	int result = 0;	
		//	request.FlightReservationModel.CombineTime();
		//	AircraftReservation aircraftReservation = _mapper.Map<FlightReservationModel, AircraftReservation>(request.FlightReservationModel);
		//	var pilot = await _context.Set<Pilot>().FindAsync(aircraftReservation.PilotId);
		//	if (pilot == null)
		//		return 0;
		//	var reservations =  _context.Set<AircraftReservation>().AsNoTracking<AircraftReservation>();
		//	var exist = reservations.Where(f => f.Intersect(aircraftReservation)).FirstOrDefault();
		//	if (exist != null && exist.Id != aircraftReservation.Id)
		//		return 0;
			
		//	//aircraftReservation.Id = 0;
		//	try
		//	{
		//		aircraftReservation.ReservationInfo = "Yossi"; //new UserInfo(pilot).GetJason();
		//		_context.Set<AircraftReservation>().Update(aircraftReservation);
				
		//		if (true)
		//		{
		//			result = await _context.SaveChangesAsync(cancellationToken);
		//		}
		//	}
		//	catch(Exception ex)
		//	{
		//		System.Diagnostics.Debug.WriteLine(ex.Message);
		//	}
		//	return result;
		//}
	}

	public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
	{
		public UpdateReservationCommandValidator()
		{
			RuleFor(x => x.FlightReservationModel.Id).GreaterThan(0).WithMessage("ReservationId Must Be Greter Then 0");
			RuleFor(x => x.FlightReservationModel).Must(DateCheck).WithMessage("Date To  Must Be Greater Then Date From");
		}
		public  bool DateCheck(FlightReservationModel flightReservationModel)
		{
			var result = flightReservationModel.DateTo > flightReservationModel.DateFrom;
			return result;
		}
	}
}
