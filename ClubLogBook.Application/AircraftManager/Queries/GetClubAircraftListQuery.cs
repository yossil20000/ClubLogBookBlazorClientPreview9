using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClubLogBook.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Application.Models;
using System.Collections.Generic;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Application.Common.Models;

namespace ClubLogBook.Application.AircraftManager.Queries
{
	
	public class GetClubAircraftListQuery : IRequest<AircraftListModel>
	{
		
		public GetClubAircraftListQuery(QueryBy queryBy, string clubName, int id)
		{
			QueryBy = queryBy;
			ClubName = clubName;
			Id = id;
		}
		public int Id { get; set; }
		public QueryBy QueryBy { get; set; }   
		public string ClubName { get; set; } = "";
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
	}
	public class GetClubAircraftListQueryHandler : IRequestHandler<GetClubAircraftListQuery, AircraftListModel>
	{
		private readonly IApplicationDbContext context;
		private readonly IMapper mapper;
		public GetClubAircraftListQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<AircraftListModel> Handle(GetClubAircraftListQuery request, CancellationToken cancellationToken)
		{

			var ar = request.QueryBy switch
			{
				QueryBy.Name => await context.Set<Club>().Where(a => (request.ClubName == "" ? true : a.Name.ToUpper() == request.ClubName.ToUpper())).SelectMany(a => a.Aircrafts).ToListAsync(),
				QueryBy.ID => await context.Set<Club>().Where(a => (request.Id == 0 ? true : a.Id == request.Id)).SelectMany(a => a.Aircrafts).ToListAsync()
			};
			
			List<AircraftModel> aircraftViewModel = new List<AircraftModel>();
			mapper.Map(ar, aircraftViewModel);
			return new AircraftListModel
			{
				//AircraftList = await context.Set<Aircraft>().ProjectTo<AircraftViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken)
				AircraftList = aircraftViewModel
			};
		}
	}
}
