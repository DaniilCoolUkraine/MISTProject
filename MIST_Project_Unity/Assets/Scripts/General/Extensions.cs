using System;
using MistProject.Requests.Response;

namespace MistProject.General
{
    public static class Extensions
    {
        public static byte[] ToByte(this string me)
        {
            return System.Text.Encoding.UTF8.GetBytes(me);
        }

        public static string GetText(this IResponseData rawData)
        {
            return System.Text.Encoding.UTF8.GetString(rawData.Data);
        }

        public static string ToTwelveHoursFormat(this string twentyFourHours)
        {
            DateTime dateTime = DateTime.ParseExact(twentyFourHours, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            return dateTime.ToString("h tt", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}