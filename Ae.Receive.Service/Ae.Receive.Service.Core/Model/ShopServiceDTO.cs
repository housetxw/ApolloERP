using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    public class ShopServiceDTO
    {
        /// <summary>
        /// 预约项目Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 预约项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否勾选
        /// </summary>
        public bool IsCheck { get; set; }
    }
}
