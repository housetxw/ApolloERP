using System;
using System.Collections.Generic;
using System.Text;
using Ae.User.Service.Common.Constant;

namespace Ae.User.Service.Common.Extension
{
    public static class DateTimeExtension
    {
        public static string GetTodayStartTime()
        {
            return Convert.ToDateTime(DateTime.Now.ToString("D")).ToString();
        }

        public static string GetTodayEndTime()
        {
            return Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("D")).AddSeconds(-1).ToString();
        }

        public static string GetFirstDayTimeOfMonth()
        {
            return DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString();
        }

        public static string GetLastDayTimeOfMonth()
        {
            return DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1).ToString();
        }

        /// <summary>
        /// yyyy-MM-dd 00:00:00
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime GetStartTimeOfDay(this DateTime value)
        {
            return value.Date;
        }

        /// <summary>
        /// yyyy-MM-dd 23:59:59
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime GetEndTimeOfDay(this DateTime value)
        {
            return value.Date.AddDays(CommonConstant.One).AddTicks(-CommonConstant.One)
                .Truncate(TimeSpan.FromMilliseconds(CommonConstant.One));
        }

        public static DateTime Truncate(this DateTime value, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero) return value;
            if (value == DateTime.MinValue || value == DateTime.MaxValue) return value;
            return value.AddTicks(-(value.Ticks % timeSpan.Ticks));
        }

        /// <summary>
        /// yyyy-MM-dd 00:00:00
        /// </summary>
        /// <param name="value"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static DateTime GetSpecificDateTimeFromDay(this DateTime value, int day)
        {
            return value.Date.AddDays(day).GetStartTimeOfDay();
        }

    }
}
