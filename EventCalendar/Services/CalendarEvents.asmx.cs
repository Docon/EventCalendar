using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using EventCalendar.Data.CalendarEventProviders;
using EventCalendar.Data.Models;

namespace EventCalendar.services
{
    /// <summary>
    /// Gets events from a data provider for a given month or day
    /// </summary>
    [WebService(Namespace = "http://www.actionarts.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CalendarEvents : System.Web.Services.WebService
    {
        [WebMethod]
        public CalendarArgs GetMonthEvents(CalendarArgs args)
        {
            ICalendarEventProvider calendarProvider = new GoogleCalendarProvider();
            args.EventDate = NormalizeUtc(args.EventDate);

            IEnumerable<ICalendarEvent> events = GetCalendarEventsForMonth(calendarProvider, args.EventDate.Year, args.EventDate.Month);

            //events.AddRange(GetReccurringEventsForMonth(calendarProvider, args.EventDate.Year, args.EventDate.Month)
            //      .SelectMany(e => GetOccurancesInMonth(e, args.EventDate)));

            //events.AddRange(GetMultidayEventsForMonth(calendarProvider, args.EventDate.Year,args.EventDate.Month)
            //      .SelectMany(e => GetOccurancesInMonth(e, args.EventDate)));

            //serialize events into the required format for the JSON string
            args.MonthEvents = SerializeEventsForCalendarioJSON(events,2);
            return args;
        }

        private static IEnumerable<ICalendarEvent> GetCalendarEventsForMonth(ICalendarEventProvider calendarProvider, int year, int month)
        {
            DateTime monthStart = new DateTime(year, month, 1, 0, 0, 0);
            DateTime monthEnd = new DateTime(year, month, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

            IEnumerable<ICalendarEvent> events = calendarProvider
                .GetEventsForTimeFrame(monthStart, monthEnd)
                .OrderBy(e => e.StartTime);

            return events;
        }

        [WebMethod]
        public CalendarArgs GetCalendarDayEvents(CalendarArgs args)
        {
            ICalendarEventProvider calendarProvider = new GoogleCalendarProvider();
            args.EventDate = NormalizeUtc(args.EventDate);

            List<ICalendarEvent> events = GetCalendarEventsForDay(calendarProvider, args.EventDate);

            //IEnumerable<ICalendarEvent> recurringEventsForMonth = GetReccuringEventsForMonthW(args.EventDate.Year, args.EventDate.Month)
            //        .SelectMany(rliEvent => GetOccurancesInMonth(rliEvent, args.EventDate));
            //events.AddRange(recurringEventsForMonth.Where(e => e.StartTime.Date == args.EventDate.Date));

            //IEnumerable<ICalendarEvent> multiDayEventsForMonth = GetMultidayEventsForMonth(args.EventDate.Year, args.EventDate.Month)
            //    .SelectMany(rliEvent => GetOccurancesInMonth(rliEvent, args.EventDate));
            //events.AddRange(multiDayEventsForMonth.Where(e => e.StartTime.Date == args.EventDate.Date));

            args.DayEvents = events;

            return args;
        }

        private List<ICalendarEvent> GetCalendarEventsForDay(ICalendarEventProvider calendarProvider, DateTime day)
        {
            DateTime dayStart = new DateTime(day.Year, day.Month, day.Day, 0, 0, 0);
            DateTime dayEnd = new DateTime(day.Year, day.Month, day.Day, 23, 59, 59);

            
            IEnumerable<ICalendarEvent> events = calendarProvider
                .GetEventsForTimeFrame(dayStart, dayEnd)
                .OrderBy(e => e.StartTime);

            return events.ToList();
        }


        /// <summary>
        /// Serializes events for into the json object format required for Calendario, i.e.: 
        /// {   
        ///     '03-31-2013':'<span>event data</span>',
        ///     '04-01-2013':'<span>event data</span><span>event data</span>',
        ///     '04-10-2013':'<span>event data</span>',
        /// }
        /// </summary>
        /// <param name="events"></param>
        /// <param name="maxPerDay"></param>
        /// <returns></returns>
        private string SerializeEventsForCalendarioJSON(IEnumerable<ICalendarEvent> events, int maxPerDay)
        {
            string more = "<span class=more>More >></span>";

            List<ICalendarEvent> eventsList  = events.ToList();
            if (!eventsList.Any())
            {
                return String.Empty;
            }

            StringBuilder results = new StringBuilder();
            ICalendarEvent lastEvt = eventsList.Last();
            DateTime previousDate = DateTime.MinValue.Date;
            string dateString = String.Empty;
            StringBuilder evtString = new StringBuilder();
            int eventsPerDayCount = 0;

            foreach (ICalendarEvent e in eventsList)
            {
                if (e.StartTime.Date != previousDate)
                {
                    //check if this is the first time through - dateString will be empty
                    if (dateString != String.Empty)
                    {
                        //append the events for the previous date
                        results.AppendFormat("'{0}':'{1}{2}',", dateString, evtString, (eventsPerDayCount > maxPerDay) ? more : String.Empty);

                        //clear the events and reset count
                        evtString.Length = 0;
                        eventsPerDayCount = 0;

                    }
                    //reset the previousDate variable to this event's date
                    previousDate = e.StartTime.Date;

                    dateString = e.StartTime.ToString("MM-dd-yyyy");
                    evtString.AppendFormat(GetMonthEvent(e));
                    eventsPerDayCount++;
                    
                }
                else
                {
                    if (eventsPerDayCount < maxPerDay)
                    {
                        evtString.Append(GetMonthEvent(e));
                    }
                    eventsPerDayCount++;
                }

                if (e.Equals(lastEvt) && dateString != String.Empty)
                {
                    results.AppendFormat("'{0}':'{1}{2}',", dateString, evtString, (eventsPerDayCount > maxPerDay) ? more : String.Empty);
                }

            }
            //delete trailing comma
            results.Length = results.Length - 1;
            return results.ToString();
        }

        /// <summary>
        /// Gets HTML for the Day cell of the Calendar month view
        /// </summary>
        /// <param name="evt">the Event</param>
        /// <returns></returns>
        private string GetMonthEvent(ICalendarEvent evt)
        {
            //For the Month titles, we need to shorten for the Day cells on the Calendar view
            string title = ShortenString(evt.Title, 30);
            return String.Format("<span>{0}</span>", title);
        }

        private DateTime NormalizeUtc(DateTime dttm)
        {
            DateTime convertedDate = TimeZoneInfo.ConvertTimeToUtc(dttm, TimeZoneInfo.Local);
            return convertedDate;
        }

        public static string ShortenString(string text, int length)
        {
            if (length < 1)
            {
                return text;
            }

            return (text.Length > length ? text.Substring(0, length) + "..." : text);
        }
    }
}
