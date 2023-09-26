using System;
using System.Net.Mail;
using System.Text;

namespace HelthOrdinations.Core.Helpers
{
    public static class ConverFromDatetimeToInt
    {
        public static int ConvertDatetimeToInt(DateTime date)
        {
            string dateString = date.ToString();

            DateTime parsedDate = DateTime.Parse(dateString);
            int hour = parsedDate.Hour;
            return hour;

        }

    }
}

