using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class AddTechTripReturnRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "ID必须大于0")]
        public long Id { get; set; }
        /// <summary>
        /// 结束公里数
        /// </summary>
        public int EndMileage { get; set; }
        /// <summary>
        /// 结束里程图片
        /// </summary>
        public string EndMileageImg { get; set; } = string.Empty;
        /// <summary>
        /// 还车油耗 （格）
        /// </summary>
        public int EndOil { get; set; }
        /// <summary>
        /// 还车油耗图片
        /// </summary>
        public string EndOilImg { get; set; } = string.Empty;
        /// <summary>
        /// 加油L
        /// </summary>
        public decimal Refuelled { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
    }
}
