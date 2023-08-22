using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FileUpload.Api.Core.Model
{
    public class SwAlarmRequest
    {
        public int scopeId { get; set; }
        public string scope { get; set; }
        public string name { get; set; }
        public int id0 { get; set; }
        public int id1 { get; set; }
        public string ruleName { get; set; }
        public string alarmMessage { get; set; }
        public long startTime { get; set; }
    }
}
