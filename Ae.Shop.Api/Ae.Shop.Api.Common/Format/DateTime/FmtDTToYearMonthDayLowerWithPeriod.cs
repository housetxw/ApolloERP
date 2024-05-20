using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;

namespace Ae.Shop.Api.Common.Format
{
    public class FmtDTToYearMonthDayLowerWithPeriod : IsoDateTimeConverter
    {
        public FmtDTToYearMonthDayLowerWithPeriod()
        {
            base.DateTimeFormat = "yyyy.M.d";
        }
    }
}
