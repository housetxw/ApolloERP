using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    /// <summary>
    /// 门店配置
    /// </summary>
    public class ShopConfigDTO
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店认证
        /// </summary>
        public int ShopCertification { get; set; }
        /// <summary>
        /// 营业开始时间
        /// </summary>
        public DateTime StartWorkTime { get; set; } = new DateTime(1900,1,1);
        /// <summary>
        /// 营业结束时间
        /// </summary>
        public DateTime EndWorkTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 税号
        /// </summary>
        public string TaxNumber { get; set; } = string.Empty;
        /// <summary>
        /// 合同类型
        /// </summary>
        public int ContractType { get; set; }
        /// <summary>
        /// 轮胎/保养服务
        /// </summary>
        public string Service { get; set; } = string.Empty;
        /// <summary>
        /// 服务等级
        /// </summary>
        public int SerivceLevel { get; set; }
        /// <summary>
        /// 技术水平
        /// </summary>
        public string TireTech { get; set; } = string.Empty;
        /// <summary>
        /// 收款方式
        /// </summary>
        public string PaymentMethod { get; set; } = string.Empty;
        /// <summary>
        /// 换轮胎费用
        /// </summary>
        public decimal FeePerTire { get; set; }
        /// <summary>
        /// 前刹车片费用
        /// </summary>
        public decimal FeePerFrontBrake { get; set; }
        /// <summary>
        /// 后刹车片费用
        /// </summary>
        public decimal FeePerRearBrake { get; set; }
        /// <summary>
        /// 前刹车盘费用
        /// </summary>
        public decimal FeePerFrontDisc { get; set; }
        /// <summary>
        /// 后刹车盘费用
        /// </summary>
        public decimal FeePerRearDisc { get; set; }
        /// <summary>
        /// 保养服务费用
        /// </summary>
        public decimal FeePerMaintain { get; set; }
        /// <summary>
        /// 四轮定位费用
        /// </summary>
        public decimal FeePer4Wheel { get; set; }
        /// <summary>
        /// PM2.5滤芯安装费用
        /// </summary>
        public decimal FeePmInstallation { get; set; }
        /// <summary>
        /// 每日保养约单量
        /// </summary>
        public int DailyOrderQuantity { get; set; }
        /// <summary>
        /// 每日保养爆单量
        /// </summary>
        public int DailyOrderUpperLimit { get; set; }
        /// <summary>
        /// 保养爆单量/约单量比例
        /// </summary>
        public decimal OptionOrderCount { get; set; }
        /// <summary>
        /// 每日轮胎约单量
        /// </summary>
        public int DailyTireOrderQuantity { get; set; }
        /// <summary>
        /// 每日轮胎爆单量
        /// </summary>
        public int DailyTireOrderUpperLimit { get; set; }
        /// <summary>
        /// 轮胎爆单量/约单量比例
        /// </summary>
        public decimal OptionTireOrderCount { get; set; }
        /// <summary>
        /// 正式上线时间
        /// </summary>
        public DateTime OnlineTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 租赁期
        /// </summary>
        public int LeaseFreePeriod { get; set; }
        /// <summary>
        /// 租赁开始日期
        /// </summary>
        public DateTime LeaseStartDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 租赁结束日期
        /// </summary>
        public DateTime LeaseEndDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 是否是VIP门店
        /// </summary>
        public bool IsVipShop { get; set; }
        /// <summary>
        /// 是否是默认门店
        /// </summary>
        public sbyte IsDefaultShop { get; set; }
        /// <summary>
        /// 汽车故障诊断仪
        /// </summary>
        public sbyte CarFaultDiagnosticTool { get; set; }
        /// <summary>
        /// 是否有休息室
        /// </summary>
        public sbyte IsLoungeRoom { get; set; }
        /// <summary>
        /// 是否有卫生间
        /// </summary>
        public sbyte IsRestRoom { get; set; }
        /// <summary>
        /// 是否免费wifi
        /// </summary>
        public sbyte IsFreeWifi { get; set; }
        /// <summary>
        /// 是否柱式举升机
        /// </summary>
        public sbyte IsPostLift { get; set; }
        /// <summary>
        /// 对接账号
        /// </summary>
        public string JointAccount { get; set; } = string.Empty;
        /// <summary>
        /// 对接密码
        /// </summary>
        public string JointPassword { get; set; } = string.Empty;
        /// <summary>
        /// 是否铺货 0：否 1：是
        /// </summary>
        public sbyte IsDistribute { get; set; }
        /// <summary>
        /// 轮保负责人
        /// </summary>
        public string LunbaoResponsiblePerson { get; set; } = string.Empty;
        /// <summary>
        /// 轮保负责人电话
        /// </summary>
        public string LunbaoResponsiblePersonTel { get; set; } = string.Empty;
        /// <summary>
        /// 美容负责人
        /// </summary>
        public string MeirongResponsiblePerson { get; set; } = string.Empty;
        /// <summary>
        /// 美容负责人电话
        /// </summary>
        public string MeirongResponsiblePersonTel { get; set; } = string.Empty;
        /// <summary>
        /// 暂停营业 开始时间
        /// </summary>
        public DateTime SuspendStartDateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 暂停营业 结束时间
        /// </summary>
        public DateTime SuspendEndDateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 是否需要安装确认码
        /// </summary>
        public sbyte IsCheckInstallCode { get; set; }
        /// <summary>
        /// 是否创建老板账户 0否 1是
        /// </summary>
        public sbyte IsCreateAccount { get; set; }
        /// <summary>
        /// 是否发送短信通知 0否 1是
        /// </summary>
        public sbyte IsSendMessage { get; set; }

        /// <summary>
        /// 是否删除 0未删除 1已删除
        /// </summary>
        public int IsDeleted { get; set; }
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
    }
}
