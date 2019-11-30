using ClubLogBook.Application.Common.Interfaces;
using System;

namespace ClubLogBook.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
