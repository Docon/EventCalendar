using System;
using System.Collections.Generic;
using EventCalendar.Data.Models;

namespace EventCalendar.Data.CalendarEventProviders
{
    public interface ICalendarEventProvider
    {
        List<CalendarEvent> GetEventsForTimeFrame(DateTime startTime, DateTime endTime);
    }
}
