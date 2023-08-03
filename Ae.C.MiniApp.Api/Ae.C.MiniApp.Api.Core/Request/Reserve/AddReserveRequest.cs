using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class AddReserveRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 车ID
        /// </summary>
        [Required(ErrorMessage = "车ID不能为空")]
        public string CarId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        [Required(ErrorMessage = "预约年不能为空")]
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
        public int IsWait { get; set; }
    }
}
