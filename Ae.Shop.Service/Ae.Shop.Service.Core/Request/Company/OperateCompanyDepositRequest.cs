using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.Company
{
    /// <summary>
    /// 
    /// </summary>
    public class OperateCompanyDepositRequest
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public CompanyDepositOperationEnum OperateType { get; set; }

        /// <summary>
        /// 押金值
        /// </summary>
        public decimal DepositAmount { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Required(ErrorMessage = "操作人不能为空")]
        public string SubmitBy { get; set; }

        /// <summary>
        /// 备注：可以放订单号之类的
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 操作押金枚举
    /// </summary>
    public enum CompanyDepositOperationEnum
    {
        /// <summary>
        /// 支付押金
        /// </summary>
        [Description("支付押金")] Payment = 0,

        /// <summary>
        /// 提取押金
        /// </summary>
        [Description("提取押金")] Extract = 1
    }
}
