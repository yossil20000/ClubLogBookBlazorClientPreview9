using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClubLogBook.Application.Accounts.Commands.UpdateAccount
{
	public class UpdateAccountInfoCommand : IRequest<int>
	{
		public int MemeberId { get; set; } = 0;
		public string MemberInfo { get; set; } = "";
		public string Description { get; set; } = "";
		public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountInfoCommand, int>
		{
			private readonly IApplicationDbContext _context;
			public UpdateAccountCommandHandler(IApplicationDbContext context) => _context = context;
			
			public async Task<int> Handle(UpdateAccountInfoCommand request, CancellationToken cancellationToken)
			{
				var entity = _context.Set<Account>().Where(val => val.MemeberId == request.MemeberId).FirstOrDefault();
				if (entity == null)
				{
					return 0;
				}
				entity.MemberInfo = request.MemberInfo;
				entity.Description = request.Description;
				_context.Set<Account>().Add(entity);

				return await _context.SaveChangesAsync(cancellationToken);
			}

		}
	}
}
