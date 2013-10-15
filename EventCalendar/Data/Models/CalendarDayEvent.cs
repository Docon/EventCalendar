using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCalendar.Data.Models
{
    public class CalendarDayEvent : ICalendarDayEvent
    {
        public CalendarDayEvent(ICalendarEvent evt)
        {
            StartTime = evt.StartTime.ToString("h:mm tt");
            Title = evt.Title;
            Description = evt.Description;
        }

        public string StartTime { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

    }
}