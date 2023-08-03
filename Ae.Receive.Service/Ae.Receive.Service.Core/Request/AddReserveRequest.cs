using Ae.Receive.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class AddReserveRequest : BaseGetRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
        /// <summary>
        /// 车ID
        /// </summary>
        public string CarId { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        [Required(ErrorMessage = "预约日期不能为空")]
        public string ReserveDate { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        [Required(ErrorMessage = "预约时间不能为空")]
        public string ReserveTime { get; set; }
        /// <summary>
        /// 是否有订单预约
        /// </summary>
        public int IsAnyOrder { get; set; }
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
        public sbyte IsWait { get; set; }
    }
}
