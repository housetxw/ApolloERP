using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model
{
    public class ReserveInfoDO
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNO { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReserveTime { get; set; }
        /// <summary>
        /// 预约编号
        /// </summary>
        public int ReserveNo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 0到店保养  1上门养护
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 是否到店等待
        /// </summary>
        public int IsWait { get; set; }
        /// <summary>
        /// 是否有订单预约
        /// </summary>
        public int IsAnyOrder { get; set; }
        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; }
        /// <summary>
        /// VIN码
        /// </summary>
        public string VinNo { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }
        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public string Nian { get; set; }
        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }
        /// <summary>
        /// 汽车logo
        /// </summary>
        public string CarLogo { get; set; }
        /// <summary>
        /// 服务大类
        /// </summary>
        public string ServiceCode { get; set; }
        /// <summary>
        /// 服务大类名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 到店时间
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 客户联系方式
        /// </summary>
        public string UserTel { get; set; }
        /// <summary>
        /// 技师名称
        /// </summary>
        public string TechName { get; set; }
        /// <summary>
        /// 渠道 1c端微信小程序 2 s端
        /// </summary>
        public int Channel { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
