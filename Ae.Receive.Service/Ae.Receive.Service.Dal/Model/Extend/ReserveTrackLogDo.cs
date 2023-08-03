using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.Extend
{
    public class ReserveTrackLogDo
    {
        public int LogId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OptType { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public string FieldName { get; set; }

        public string BeforeValue { get; set; }

        public string AfterValue { get; set; }
    }
}
