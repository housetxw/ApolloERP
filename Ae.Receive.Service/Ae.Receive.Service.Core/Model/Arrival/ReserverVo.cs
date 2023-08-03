using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Arrival
{
    /// <summary>
    /// 预约对象
    /// </summary>
    public class ReserverVo
    {
        /// <summary>
        /// 预约时间
        /// </summary>
        public string ShowReserverTime { get; set; }

        /// <summary>
        /// 显示的预约信息
        /// </summary>
        public string ShowReserverInfo { get; set; }

        /// <summary>
        /// 预约备注
        /// </summary>
        public string ReserverRemark { get; set; }

        /// <summary>
        /// 图片信息集合
        /// </summary>
        public List<ImgVo> Imgs { get; set; }
    }
}
