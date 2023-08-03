using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.Extend
{
    public class ShopReserveRelationDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 预约到店时间
        /// </summary>
        public DateTime ReserveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 预约编号
        /// </summary>
        public int ReserveNo { get; set; }
        /// <summary>
        /// 是否到店等待
        /// </summary>
        public sbyte IsWait { get; set; }
        /// <summary>
        /// 是否有订单预约
        /// </summary>
        public int IsAnyOrder { get; set; }
        /// <summary>
        /// 渠道 1小程序 2门店
        /// </summary>
        public int Channel { get; set; }
        /// <summary>
        /// 状态 0待确认 1已确认 2已完成 3已取消
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 客户联系方式
        /// </summary>
        public string UserTel { get; set; } = string.Empty;
        /// <summary>
        /// 客户性别
        /// </summary>
        public string UserSex { get; set; } = string.Empty;
        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// VIN码
        /// </summary>
        public string VinNo { get; set; } = string.Empty;
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
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; } = string.Empty;

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
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; } = string.Empty;

        /// <summary>
        /// 款型
        /// </summary>
        public string CarLogo { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 工位号
        /// </summary>
        public long StationId { get; set; }
        /// <summary>
        /// 技师Id
        /// </summary>
        public string TechId { get; set; } = string.Empty;
        /// <summary>
        /// 技师姓名
        /// </summary>
        public string TechName { get; set; } = string.Empty;
        /// <summary>
        /// 服务大类
        /// </summary>
        public string ServiceCode { get; set; } = string.Empty;
        /// <summary>
        /// 服务大类名称
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 取消人
        /// </summary>
        public string CancelBy { get; set; } = string.Empty;
        /// <summary>
        /// 取消时间
        /// </summary>
        public DateTime CancelTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);

        public string ServiceType { get; set; } = string.Empty;


        public DateTime? ArriveTime { get; set; }
    }
}
