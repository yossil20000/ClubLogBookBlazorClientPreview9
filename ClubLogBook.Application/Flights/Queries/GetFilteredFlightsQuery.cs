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
using ClubLogBook.Application.ViewModels;
using ClubLogBook.Core.Interfaces;
using System;

namespace ClubLogBook.Application.Flights.Queries
{
	
	public class GetFilteredFlightsQuery : IRequest<FlightRecordIndexViewModel>
	{
		public FlightRecordIndexViewModel flightRecordIndexView { get; set; }
		
		public class GetFilteredFlightsHandler : IRequestHandler<GetFilteredFlightsQuery, FlightRecordIndexViewModel>
		{
			private readonly IApplicationDbContext _context;
			private readonly IMapper _mapper;
			public GetFilteredFlightsHandler(IApplicationDbContext context, IMapper mapper) => (_mapper, _context) = (mapper, context);


			public async Task<FlightRecordIndexViewModel> Handle(GetFilteredFlightsQuery request, CancellationToken cancellationToken)
			{
				InvoiceToFlightComaprer comparer = new InvoiceToFlightComaprer();
				
				//int ActualPage = request.PaginationInfoViewModel.ActualPage;
				//switch (request.PaginationInfoViewModel.PageCommand)
				//{
				//	case PageCommand.MoveNext:
				//		ActualPage = request.PaginationInfoViewModel.ActualPage + 1;
				//		break;
				//	case PageCommand.MovePrevious:
				//		ActualPage = request.PaginationInfoViewModel.ActualPage - 1;
				//		break;
				//}
				DateTime fromDate = request.flightRecordIndexView.FilterViewModel.FilterDateViewModel.FilterDateFrom;
				DateTime toDate = request.flightRecordIndexView.FilterViewModel.FilterDateViewModel.FilterDateTo;
				var clubId = request.flightRecordIndexView.FilterViewModel.ClubFilterApplied ?? 0;
				var aircraftId = request.flightRecordIndexView.FilterViewModel.AirplaneFilterApplied ?? 0;
				var pilotId = request.flightRecordIndexView.FilterViewModel.PilotFilterApplied ?? 0;
				List<int> aircraftIds = aircraftId == 0 ? _context.Set<Aircraft>().Select(i => i.Id).ToList() : new List<int>() { aircraftId };
				List<int> pilotIds = pilotId == 0 ? _context.Set<Member>().Select(i => i.Id).ToList() : new List<int>() { pilotId };
				var aircraftLogBook = from lb in _context.Set<AircraftLogBook>()
									  join ac in _context.Set<Aircraft>() on lb.TaiNumber equals ac.TailNumber
									  where aircraftIds.Contains(ac.Id)
									  select lb;
				
				var club = _context.Set<Club>().Find(clubId);
				var flights = (_context.Set<Club>().Where(c => c.Id == clubId)).Select(a => a.Aircrafts).ToList();
				//var filteredFlight = (from fl in _context.Set<Flight>()
				//		 join ar in _context.Set<Aircraft>() on fl.Aircraft.Id equals ar.Id
				//		 where fl.Pilot.Id == pilotId && fl.Date >= fromDate && fl.Date <= toDate
				//		 select fl).ToList();

				var filteredFlight = (from fl in _context.Set<Flight>()
									 
									  where ( fl.Date >= fromDate && fl.Date <= toDate  && aircraftIds.Contains(fl.Aircraft.Id) && pilotIds.Contains(fl.Pilot.Id))
									  select fl).ToList();

				int totalFlight = filteredFlight.Count();
				request.flightRecordIndexView.PaginationInfo.TotalItems = filteredFlight.Count();
				request.flightRecordIndexView.PaginationInfo.ActualPage = 1;
				
				request.flightRecordIndexView.PaginationInfo.TotalItems = totalFlight;
				request.flightRecordIndexView.PaginationInfo.TotalPages = int.Parse(Math.Ceiling((decimal)totalFlight / request.flightRecordIndexView.PaginationInfo.ItemsPerPage).ToString());
				request.flightRecordIndexView.PaginationInfo.Next = request.flightRecordIndexView.PaginationInfo.ActualPage >= request.flightRecordIndexView.PaginationInfo.TotalPages  ? "disabled" : "";
				request.flightRecordIndexView.PaginationInfo.Previous = request.flightRecordIndexView.PaginationInfo.ActualPage == 1 ? "disabled" : "";
				request.flightRecordIndexView.FlightRecords = _mapper.Map<List<Flight>, List<ClubFlightViewModel>>(filteredFlight);
				return request.flightRecordIndexView;
			}
		}

		public class GetFilteredFlightsQueryValidator : AbstractValidator<GetFilteredFlightsQuery>
		{
			public GetFilteredFlightsQueryValidator()
			{
				//RuleFor(x => x.flightRecordIndexView.FilterViewModel.AirplaneFilterApplied).GreaterThan(0).WithMessage("AirplaneFilterApplied Must Be > 0");
				//RuleFor(x => x.flightRecordIndexView.FilterViewModel.ClubFilterApplied).GreaterThan(0).WithMessage("ClubFilterApplied Must Be > 0");
				//RuleFor(x => x.flightRecordIndexView.FilterViewModel.PilotFilterApplied).GreaterThan(0).WithMessage("PilotFilterApplied Must Be > 0");
				RuleFor(x => x.flightRecordIndexView.FilterViewModel.FilterDateViewModel).MustAsync(DateCheck).WithMessage("Date To Must Be Greater The Date From");


			}
			public async Task<bool> DateCheck(FilterDateViewModel filterDateViewModel,CancellationToken ct)
			{
				return  filterDateViewModel.FilterDateTo >= filterDateViewModel.FilterDateFrom;
			}
		}

	}
}
