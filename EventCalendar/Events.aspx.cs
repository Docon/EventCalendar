using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using EventCalendar.Data.CalendarEventProviders;
using EventCalendar.Data.Models;

namespace EventCalendar
{
	public partial class Events : System.Web.UI.Page
	{
	    
        private static bool ShowEventListBelowCalendar 
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["ShowEventListBelowCalendar"]);
                }
                catch
                {
                    return false;
                }
            }
        }

		protected void Page_Load(object sender, EventArgs e)
		{
		}

	}
}