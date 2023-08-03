using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;

namespace Ae.C.MiniApp.Api.Common.Format
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
