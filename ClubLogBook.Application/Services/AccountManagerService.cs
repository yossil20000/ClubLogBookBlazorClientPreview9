using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Application.Interfaces;
namespace ClubLogBook.Application.Services
{
	public class AccountManagerService : IAccountManagerService
	{
		IAppLogger<AccountManagerService> _appLogger;
		public AccountManagerService(IAppLogger<AccountManagerService> appLogger)
		{
			_appLogger = appLogger;

		}
	}
}
