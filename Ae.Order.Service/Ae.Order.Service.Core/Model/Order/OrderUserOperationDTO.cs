﻿using Ae.Order.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Model.Order
{
    public class OrderUserOperationDTO
    {
        /// <summary>
        /// 订单用户可执行操作枚举
        /// </summary>
        public OrderUserOperationEnum Function { get; set; }

        /// <summary>
        /// 展示的序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否突出
        /// </summary>
        public bool IsImportance { get; set; }
    }
}
