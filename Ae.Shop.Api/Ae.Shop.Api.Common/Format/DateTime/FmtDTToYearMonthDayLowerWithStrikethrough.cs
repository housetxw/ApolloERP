using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;

namespace Ae.Shop.Api.Common.Format.DateTime
{
    public class FmtDTToYearMonthDayLowerWithStrikethrough : IsoDateTimeConverter
    {
        public FmtDTToYearMonthDayLowerWithStrikethrough()
        {
            base.DateTimeFormat = "yyyy-M-d";
        }
    }
}
