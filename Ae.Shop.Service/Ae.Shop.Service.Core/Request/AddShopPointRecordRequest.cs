using Ae.Shop.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    /// <summary>
    /// 新增门店积分Request
    /// </summary>
    public class AddShopPointRecordRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1,Int32.MaxValue,ErrorMessage ="ShopId不能为空")]
        public int ShopId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public ShopPointOperateTypeEnum OperateType { get; set; }

        /// <summary>
        /// 操作类型描述（Eg：邀请xxx注册   xxx是门店老板名字）
        /// </summary>
        public string OperateTypeDescription { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        public int PointValue { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "操作人不能为空")]
        public string SubmitBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
