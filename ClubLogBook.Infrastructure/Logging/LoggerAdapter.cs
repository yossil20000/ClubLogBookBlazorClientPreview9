using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Application.Interfaces;
using Microsoft.Extensions.Logging;
namespace ClubLogBook.Infrastructure.Logging
{
	public class LoggerAdapter<T> : IAppLogger<T>
	{
		private readonly ILogger<T> _logger;

		public LoggerAdapter(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<T>();
		}

		void IAppLogger<T>.LogInformation(string message, params object[] args)
		{
			_logger.LogInformation(message, args);
		}

		void IAppLogger<T>.LogWorning(string message, params object[] args)
		{
			_logger.LogWarning(message, args);
		}
	}
}
