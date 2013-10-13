using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Google.GData.Calendar;
using Google.GData.Client;

namespace EventCalendar
{
	public partial class Events : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var query = new EventQuery("https://www.google.com/calendar/feeds/gdiprogrammingclass@gmail.com/public/full");
			var service = new CalendarService("GDI Calendar");
			service.setUserCredentials("gdiprogrammingclass", "gdidemo4class");

			query.StartTime = DateTime.Now.AddDays(-28.0);
			query.EndTime = DateTime.Now.AddMonths(6);
			EventFeed feed = service.Query(query);

			//ListEvents(feed);
		    ltlData.Text = BuildEventsToJson(feed);
		}

		private void ListEvents(EventFeed feed)
		{
			var sb = new StringBuilder("<ul>");
			foreach (EventEntry entry in feed.Entries)
			{
				sb.AppendFormat("<li><strong>{0} - {1}</strong> {2}<br/>{3}</li>", 
					entry.Times[0].StartTime.ToString("M/d/yyyy h:mm tt"),
					entry.Times[0].EndTime.ToString("M/d/yyyy h:mm tt"), 
					entry.Title,
					entry.Summary);
				//static utility method for Start and End times to format 
			}
			sb.Append("</ul><br/>");
			ltlResults.Text = sb.ToString();
		}

		private string BuildEventsToJson(EventFeed feed)
		{
            /* Example of JSON:
             * var gdiEvents = {
                '9-23-2013' : '<a href="http://www.cnn.com/">My Test Event</a>',
                '10-21-2013' : '<a href="http://www.cnn.com/">My Test Event 2</a>'
             * }
             * */

            var sb = new StringBuilder("<script language=\"javascript\">");
			sb.AppendLine("var gdiEvents = {");
			foreach (EventEntry entry in feed.Entries)
			{
				sb.AppendFormat("'{0}':'{1}',",
					entry.Times[0].StartTime.ToString("MM-dd-yyyy"),
					entry.Title.Text);
			}
			//Trim trailing comma
			sb.Length = sb.Length - 1;

            sb.AppendLine("};");
		    sb.AppendLine("</script>");
			
			return sb.ToString();
		}

	}
}