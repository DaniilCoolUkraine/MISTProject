using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MistProject.Requests.Response;
using UnityEngine;

namespace MistProject.General
{
    public static class Extensions
    {
        public static byte[] ToByte(this string me)
        {
            return Encoding.UTF8.GetBytes(me);
        }

        public static string GetText(this IResponseData rawData)
        {
            return Encoding.UTF8.GetString(rawData.Data);
        }

        public static string ToTwelveHoursFormat(this string twentyFourHours)
        {
            DateTime dateTime = DateTime.ParseExact(twentyFourHours, "HH:mm", CultureInfo.InvariantCulture);
            return dateTime.ToString("h tt", CultureInfo.InvariantCulture);
        }
        
        public static bool ListIsEmptyOrNull<T>(this List<T> list)
        {
            return list == null && list.Count == 0;
        }

        public static string ToAppDate(this string date)
        {
            DateTime result = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var sb = new StringBuilder();

            return sb.Append(result.DayOfWeek).Append(", ").Append(result.Day).Append(" ")
                .Append(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(result.Month)).ToString();
        }

        public static void KillAllChildren<T>(this Transform parent) where T: Component
        {
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent<T>(out _))
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }
        }
    }
}