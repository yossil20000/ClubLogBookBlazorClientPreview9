using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Application.Interfaces
{
	public interface IAppLogger<T>
	{
		void LogInformation(string message, params object[] args);
		void LogWorning(string message, params object[] args);
	}
}
