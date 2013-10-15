using System;
using System.Collections.Generic;
using System.Configuration;
using EventCalendar.Data.Models;
using Google.GData.Calendar;

namespace EventCalendar.Data.CalendarEventProviders
{
    public class GoogleCalendarProvider : ICalendarEventProvider
    {
        private static readonly string ApplicationName = ConfigurationManager.AppSettings["CalendarApplicationName"];
        private static readonly string Account = ConfigurationManager.AppSettings["CalendarProviderAccount"];
        private static readonly string Password = ConfigurationManager.AppSettings["CalendarProviderAccount"];
        private static readonly string QueryUrl = ConfigurationManager.AppSettings["CalendarQueryUrl"];

        public List<CalendarEvent> GetEventsForTimeFrame(DateTime startTime, DateTime endTime)
        {
            var events = new List<CalendarEvent>();

            var service = new CalendarService(ApplicationName);
            service.setUserCredentials(Account, Password);

            var query = new EventQuery(QueryUrl);
            query.StartTime = startTime;
            query.EndTime = endTime;
            EventFeed feed = service.Query(query);

            foreach (EventEntry entry in feed.Entries)
            {
                if (entry == null)
                {
                    continue;
                }
                CalendarEvent calendarEvent = MapToCalendarEvent(entry);
                events.Add(calendarEvent);
            }
            return events;
        }

        private CalendarEvent MapToCalendarEvent(EventEntry entry)
        {
            var calendarEvent = new CalendarEvent();
            calendarEvent.StartTime = entry.Times[0].StartTime;
            calendarEvent.EndTime = entry.Times[0].EndTime;
            calendarEvent.Title = entry.Title.Text;
            calendarEvent.Description = entry.Summary.Text;
            calendarEvent.Description = String.Join(",", entry.Locations);
            return calendarEvent;
        }
    }
}