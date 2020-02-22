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
		public FilterViewModel FilterViewModel { get; set; }
		
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
				DateTime fromDate = request.FilterViewModel.FilterDateViewModel.FilterDateFrom;
				DateTime toDate = request.FilterViewModel.FilterDateViewModel.FilterDateTo;
				var clubId = request.FilterViewModel.ClubFilterApplied ?? 0;
				var aircraftId = request.FilterViewModel.AirplaneFilterApplied ?? 0;
				var pilotId = request.FilterViewModel.PilotFilterApplied ?? 0;
				//var club = _context.Set<Club>().Find(clubId);
				//var flights = (_context.Set<Club>().Where(c => c.Id == clubId)).Select(a => a.Aircrafts).ToList();
				var bb = (from fl in _context.Set<Flight>()
						 join ar in _context.Set<Aircraft>() on fl.Aircraft.Id equals ar.Id
						 where fl.Pilot.Id == pilotId && fl.Date >= fromDate && fl.Date <= toDate
						 select fl).ToList();



				FlightRecordIndexViewModel flightRecordIndexViewModel = new FlightRecordIndexViewModel();
				flightRecordIndexViewModel.FlightRecords = _mapper.Map<List<Flight>, List<ClubFlightViewModel>>(bb);
				return flightRecordIndexViewModel;
			}
		}

		public class GetFilteredFlightsQueryValidator : AbstractValidator<GetFilteredFlightsQuery>
		{
			public GetFilteredFlightsQueryValidator()
			{
				RuleFor(x => x.FilterViewModel.AirplaneFilterApplied).GreaterThan(0).WithMessage("AirplaneFilterApplied Must Be > 0");
				RuleFor(x => x.FilterViewModel.ClubFilterApplied).GreaterThan(0).WithMessage("ClubFilterApplied Must Be > 0");
				RuleFor(x => x.FilterViewModel.PilotFilterApplied).GreaterThan(0).WithMessage("PilotFilterApplied Must Be > 0");
				RuleFor(x => x.FilterViewModel.FilterDateViewModel).MustAsync(DateCheck).WithMessage("Date To Must Be Greater The Date From");


			}
			public async Task<bool> DateCheck(FilterDateViewModel filterDateViewModel,CancellationToken ct)
			{
				return  filterDateViewModel.FilterDateTo >= filterDateViewModel.FilterDateFrom;
			}
		}

	}
}
