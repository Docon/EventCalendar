using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCalendar.Data.Extensions
{
    public static class StringExtensions
    {
        public static string ShortenString(this string s, int length)
        {
            if (length < 1)
            {
                return s;
            }
            return (s.Length > length ? s.Substring(0, length) + "..." : s);
        }
    }
}