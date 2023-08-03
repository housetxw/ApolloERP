using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model
{
    public class UpdateReserveDTO
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReserveDateTime { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 车ID
        /// </summary>
        public string CarId { get; set; }
        /// <summary>
        /// 预约项目Code
        /// </summary>
        public string ServiceCode { get; set; }
        /// <summary>
        /// 服务大类名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 是否到店等待
        /// </summary>
        public int IsWait { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo { get; set; } = string.Empty;
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;
        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; } = string.Empty;
        /// <summary>
        /// 年
        /// </summary>
        public string Nian { get; set; } = string.Empty;
        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; } = string.Empty;
        /// <summary>
        /// 款型
        /// </summary>
        public string CarLogo { get; set; } = string.Empty;
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
