using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class CheckShopJoinRequest
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ConfirmUser { get; set; }
        /// <summary>
        /// 审核人手机
        /// </summary>
        public string ConfirmUserPhone { get; set; }
        /// <summary>
        /// 状态  
        /// </summary>
        public int IsConfirmed { get; set; }
        /// <summary>
        /// 审核备注
        /// </summary>
        public string ConfirmRemark { get; set; }
    }
}
