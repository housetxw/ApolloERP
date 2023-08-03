using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class OrderUserOperationVO
    {
        /// <summary>
        /// 订单用户可执行操作枚举
        /// </summary>
        public OrderUserOperationEnum Function { get; set; }

        /// <summary>
        /// 订单用户可执行操作中文名
        /// </summary>
        public string ShowFunctionName { get; set; } = string.Empty;

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
