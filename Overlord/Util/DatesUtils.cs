using System;
using System.Collections.Generic;
using System.Text;

namespace Overlord.Util
{
	public static class DatesUtils
	{

        public static DateTime[] GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                allDates.Add(date);
            return allDates.ToArray();
        }

    }
}
