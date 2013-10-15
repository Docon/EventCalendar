using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCalendar.Data.Models
{
    public interface ICalendarDayEvent
    {
        string StartTime { get; set; }
        string Description { get; set; }
        string Title { get; set; }
    }
}
