using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model
{
    public class ReserveTimeDTO
    {
        /// <summary>
        /// 显示的预约时间
        /// </summary>
        public string ReserveTime { get; set; }
        /// <summary>
        /// 是否高亮
        /// </summary>
        public bool IsHighLight { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
