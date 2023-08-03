using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Request
{
   public class UpdateOrderStatusClientRequest
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public List<string> OrderNos { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新订单状态类型
        /// </summary>
        public UpdateOrderStatusTypeEnum UpdateStatusType { get; set; }
    }

    public enum UpdateOrderStatusTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        Null = 0,
        /// <summary>
        /// 1签收状态
        /// </summary>
        SignStatus = 1,
        /// <summary>
        /// 安装状态
        /// </summary>
        InstallStatus = 2,

        /// <summary>
        /// 已对账
        /// </summary>
        HaveReconciliation = 3,
        /// <summary>
        /// 异常对账
        /// </summary>
        ExceptionReconciliation = 4,
        /// <summary>
        /// 未对账
        /// </summary>
        NoReconciliation = 5
    }

}
