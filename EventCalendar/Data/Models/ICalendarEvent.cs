using System;

namespace EventCalendar.Data.Models
{
    public interface ICalendarEvent
    {
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Location { get; set; }
        int UtcOffset { get; set; }

    }
}
