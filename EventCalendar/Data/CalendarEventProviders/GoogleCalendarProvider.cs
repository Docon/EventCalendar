using System;
using System.Collections.Generic;
using System.Configuration;
using EventCalendar.Data.Models;
using Google.GData.Calendar;

namespace EventCalendar.Data.CalendarEventProviders
{
    public class GoogleCalendarProvider : ICalendarEventProvider
    {
        private static string _applicationName;
        private static string _account;
        private static string _password;
        private static string _queryUrl;

        public GoogleCalendarProvider()
        {
            _applicationName = ConfigurationManager.AppSettings["CalendarApplicationName"];
            _account = ConfigurationManager.AppSettings["CalendarProviderAccount"];
            _password = ConfigurationManager.AppSettings["CalendarProviderPassword"];
            _queryUrl = ConfigurationManager.AppSettings["CalendarQueryUrl"];
        }
        public List<CalendarEvent> GetEventsForTimeFrame(DateTime startTime, DateTime endTime)
        {
            var events = new List<CalendarEvent>();

            var service = new CalendarService(_applicationName);
            service.setUserCredentials(_account, _password);

            var query = new EventQuery(_queryUrl);
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