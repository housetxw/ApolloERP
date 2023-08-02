using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Imp.Helpers
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取本周的周一日期
        /// </summary>
        /// <returns></returns>
        public static DateTime GetThisWeekMonday()
        {
            DateTime date = DateTime.Now;
            DateTime firstDate = System.DateTime.Now;
            switch (date.DayOfWeek)
            {
                case System.DayOfWeek.Monday:
                    firstDate = date;
                    break;
                case System.DayOfWeek.Tuesday:
                    firstDate = date.AddDays(-1);
                    break;
                case System.DayOfWeek.Wednesday:
                    firstDate = date.AddDays(-2);
                    break;
                case System.DayOfWeek.Thursday:
                    firstDate = date.AddDays(-3);
                    break;
                case System.DayOfWeek.Friday:
                    firstDate = date.AddDays(-4);
                    break;
                case System.DayOfWeek.Saturday:
                    firstDate = date.AddDays(-5);
                    break;
                case System.DayOfWeek.Sunday:
                    firstDate = date.AddDays(-6);
                    break;
            }

            return firstDate;
        }
    }
}
