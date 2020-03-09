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

namespace ClubLogBook.Application.ClubContact.Queries
{
	public class PilotSelectListViewModel
	{
		public List<PilotSelectModel> PilotSelectList { get; set; } = new List<PilotSelectModel>();
	}
	
	public class GetClubMembersListQuery : IRequest<PilotSelectListViewModel>
	{

		public GetClubMembersListQuery(QueryBy queryBy, string clubName, int id)
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
	public class GetClubMembersListQueryHandler : IRequestHandler<GetClubMembersListQuery, PilotSelectListViewModel>
	{
		private readonly IApplicationDbContext context;
		private readonly IMapper mapper;
		public GetClubMembersListQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<PilotSelectListViewModel> Handle(GetClubMembersListQuery request, CancellationToken cancellationToken)
		{

			var members = request.QueryBy switch
			{
				QueryBy.Name => await context.Set<Club>().Where(a => (request.ClubName == "" ? true : a.Name.ToUpper() == request.ClubName.ToUpper())).SelectMany(a => a.Members).ToListAsync(),
				QueryBy.ID => await context.Set<Club>().Where(a => (request.Id == 0 ? true : a.Id == request.Id)).SelectMany(a => a.Members).ToListAsync()
			};

			PilotSelectListViewModel pilotSelecttListViewModel = new PilotSelectListViewModel();
			pilotSelecttListViewModel.PilotSelectList = mapper.Map<List<Pilot>, List<PilotSelectModel>>(members);
			return pilotSelecttListViewModel;
		}
	}
}
