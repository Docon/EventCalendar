using System;
using System.Collections.Generic;

namespace EventCalendar.Data.Models
{
    public interface ICalendarArgs
    {
        DateTime EventDate { get; set; }
        string MonthEvents { get; set; }
        List<ICalendarEvent> DayEvents { get; set; }
    }
}
