using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    /// <summary>
    /// 预约门店信息
    /// </summary>
    public class ReserveShopInfoDTO
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 简单名称
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 门店电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string ReserveDate { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }
        /// <summary>
        /// 是否有订单预约
        /// </summary>
        public int IsAnyOrder { get; set; }
        /// <summary>
        /// 是否到店等待
        /// </summary>
        public int IsWait { get; set; }
        /// <summary>
        /// 预约项目Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 预约项目名称
        /// </summary>
        public string Name { get; set; }
    }
}
