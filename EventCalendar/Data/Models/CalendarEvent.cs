﻿using System;

namespace EventCalendar.Data.Models
{
    public class CalendarEvent : ICalendarEvent
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int UtcOffset { get; set; }
    }
}
