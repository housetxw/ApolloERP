using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Common.Format
{
    /// <summary>
    /// Format: yyyy.MM.dd
    /// </summary>
    public class FmtDTToDateWithPeriod : IsoDateTimeConverter
    {
        public FmtDTToDateWithPeriod()
        {
            base.DateTimeFormat = "yyyy.MM.dd";
        }
    }
}
