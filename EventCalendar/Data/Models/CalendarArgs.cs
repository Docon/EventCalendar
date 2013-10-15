using System;
using System.Collections.Generic;

namespace EventCalendar.Data.Models
{
    public class CalendarArgs : ICalendarArgs
    {
        public DateTime EventDate { get; set; }
        public string MonthEvents { get; set; }
        public List<ICalendarDayEvent> DayEvents { get; set; }
    }
}