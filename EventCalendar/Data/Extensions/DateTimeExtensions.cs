using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCalendar.Data.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime NormalizeUtc(this DateTime dttm)
        {
            DateTime convertedDate = TimeZoneInfo.ConvertTimeToUtc(dttm, TimeZoneInfo.Local);
            return convertedDate;
        }
    }
}