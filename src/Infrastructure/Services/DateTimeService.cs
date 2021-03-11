using System;
using BerkeGaming.Application.Common.Interfaces;

namespace BerkeGaming.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
