using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Model.Product
{
    /// <summary>
    /// CheckDoorProductVo
    /// </summary>
    public class CheckDoorProductVo
    {
        /// <summary>
        /// Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 是否上门产品
        /// </summary>
        public bool IsDoorProduct { get; set; }
    }
}
