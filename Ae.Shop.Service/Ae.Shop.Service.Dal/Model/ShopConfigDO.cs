using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.Shop.Service.Common.Helper;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_config")]
    public class ShopConfigDO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        [CompareDiffAttribute(Name = "门店ID")]
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 门店认证
        /// </summary>
        [Column("shop_certification")]
        [CompareDiffAttribute(Name = "门店认证")]
        public int ShopCertification { get; set; }
        /// <summary>
        /// 营业开始时间
        /// </summary>
        [Column("start_work_time")]
        [CompareDiffAttribute(Name = "营业开始时间")]
        public DateTime StartWorkTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 营业结束时间
        /// </summary>
        [Column("end_work_time")]
        [CompareDiffAttribute(Name = "营业结束时间")]
        public DateTime EndWorkTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 税号
        /// </summary>
        [Column("tax_number")]
        [CompareDiffAttribute(Name = "税号")]
        public string TaxNumber { get; set; } = string.Empty;
        /// <summary>
        /// 合同类型
        /// </summary>
        [Column("contract_type")]
        [CompareDiffAttribute(Name = "合同类型")]
        public int ContractType { get; set; }
        /// <summary>
        /// 轮胎/保养服务
        /// </summary>
        [Column("service")]
        [CompareDiffAttribute(Name = "服务")]
        public string Service { get; set; } = string.Empty;
        /// <summary>
        /// 服务等级
        /// </summary>
        [Column("serivce_level")]
        [CompareDiffAttribute(Name = "服务等级")]
        public int SerivceLevel { get; set; }
        /// <summary>
        /// 技术水平
        /// </summary>
        [Column("tire_tech")]
        [CompareDiffAttribute(Name = "技术水平")]
        public string TireTech { get; set; } = string.Empty;
        /// <summary>
        /// 收款方式
        /// </summary>
        [Column("payment_method")]
        [CompareDiffAttribute(Name = "收款方式")]
        public string PaymentMethod { get; set; } = string.Empty;
        /// <summary>
        /// 换轮胎费用
        /// </summary>
        [Column("fee_per_tire")]
        [CompareDiffAttribute(Name = "换轮胎费用")]
        public decimal FeePerTire { get; set; }
        /// <summary>
        /// 前刹车片费用
        /// </summary>
        [Column("fee_per_front_brake")]
        [CompareDiffAttribute(Name = "前刹车片费用")]
        public decimal FeePerFrontBrake { get; set; }
        /// <summary>
        /// 后刹车片费用
        /// </summary>
        [Column("fee_per_rear_brake")]
        [CompareDiffAttribute(Name = "后刹车片费用")]
        public decimal FeePerRearBrake { get; set; }
        /// <summary>
        /// 前刹车盘费用
        /// </summary>
        [Column("fee_per_front_disc")]
        [CompareDiffAttribute(Name = "前刹车盘费用")]
        public decimal FeePerFrontDisc { get; set; }
        /// <summary>
        /// 后刹车盘费用
        /// </summary>
        [Column("fee_per_rear_disc")]
        [CompareDiffAttribute(Name = "后刹车盘费用")]
        public decimal FeePerRearDisc { get; set; }
        /// <summary>
        /// 保养服务费用
        /// </summary>
        [Column("fee_per_maintain")]
        [CompareDiffAttribute(Name = "保养服务费用")]
        public decimal FeePerMaintain { get; set; }
        /// <summary>
        /// 四轮定位费用
        /// </summary>
        [Column("fee_per_4_wheel")]
        [CompareDiffAttribute(Name = "四轮定位费用")]
        public decimal FeePer4Wheel { get; set; }
        /// <summary>
        /// PM2.5滤芯安装费用
        /// </summary>
        [Column("fee_pm_installation")]
        [CompareDiffAttribute(Name = " PM2.5滤芯安装费用")]
        public decimal FeePmInstallation { get; set; }
        /// <summary>
        /// 每日保养约单量
        /// </summary>
        [Column("daily_order_quantity")]
        [CompareDiffAttribute(Name = "每日保养约单量")]
        public int DailyOrderQuantity { get; set; }
        /// <summary>
        /// 每日保养爆单量
        /// </summary>
        [Column("daily_order_upper_limit")]
        [CompareDiffAttribute(Name = "每日保养爆单量")]
        public int DailyOrderUpperLimit { get; set; }
        /// <summary>
        /// 保养爆单量/约单量比例
        /// </summary>
        [Column("option_order_count")]
        [CompareDiffAttribute(Name = "保养爆单量")]
        public decimal OptionOrderCount { get; set; }
        /// <summary>
        /// 每日轮胎约单量
        /// </summary>
        [Column("daily_tire_order_quantity")]
        [CompareDiffAttribute(Name = "每日轮胎约单量")]
        public int DailyTireOrderQuantity { get; set; }
        /// <summary>
        /// 每日轮胎爆单量
        /// </summary>
        [Column("daily_tire_order_upper_limit")]
        [CompareDiffAttribute(Name = "每日轮胎爆单量")]
        public int DailyTireOrderUpperLimit { get; set; }
        /// <summary>
        /// 轮胎爆单量/约单量比例
        /// </summary>
        [Column("option_tire_order_count")]
        [CompareDiffAttribute(Name = "轮胎爆单量")]
        public decimal OptionTireOrderCount { get; set; }
        /// <summary>
        /// 正式上线时间
        /// </summary>
        [Column("online_time")]
        [CompareDiffAttribute(Name = "正式上线时间")]
        public DateTime OnlineTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 租赁期
        /// </summary>
        [Column("lease_free_period")]
        [CompareDiffAttribute(Name = "租赁期")]
        public int LeaseFreePeriod { get; set; }
        /// <summary>
        /// 租赁开始日期
        /// </summary>
        [Column("lease_start_date")]
        [CompareDiffAttribute(Name = "租赁开始日期")]
        public DateTime LeaseStartDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 租赁结束日期
        /// </summary>
        [Column("lease_end_date")]
        [CompareDiffAttribute(Name = "租赁结束日期")]
        public DateTime LeaseEndDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 是否是VIP门店
        /// </summary>
        [Column("is_vip_shop")]
        [CompareDiffAttribute(Name = "是否是VIP门店")]
        public sbyte IsVipShop { get; set; }
        /// <summary>
        /// 是否是默认门店
        /// </summary>
        [Column("is_default_shop")]
        [CompareDiffAttribute(Name = "是否是默认门店")]
        public sbyte IsDefaultShop { get; set; }
        /// <summary>
        /// 汽车故障诊断仪
        /// </summary>
        [Column("car_fault_diagnostic_tool")]
        [CompareDiffAttribute(Name = "汽车故障诊断仪")]
        public sbyte CarFaultDiagnosticTool { get; set; }
        /// <summary>
        /// 是否有休息室
        /// </summary>
        [Column("is_lounge_room")]
        [CompareDiffAttribute(Name = "是否有休息室")]
        public sbyte IsLoungeRoom { get; set; }
        /// <summary>
        /// 是否有卫生间
        /// </summary>
        [Column("is_rest_room")]
        [CompareDiffAttribute(Name = "是否有卫生间")]
        public sbyte IsRestRoom{ get; set; }
        /// <summary>
        /// 是否免费wifi
        /// </summary>
        [Column("is_free_wifi")]
        [CompareDiffAttribute(Name = "是否免费wifi")]
        public sbyte IsFreeWifi { get; set; }
        /// <summary>
        /// 是否柱式举升机
        /// </summary>
        [Column("is_post_lift")]
        [CompareDiffAttribute(Name = "是否柱式举升机")]
        public sbyte IsPostLift { get; set; }
        /// <summary>
        /// 对接账号
        /// </summary>
        [Column("joint_account")]
        [CompareDiffAttribute(Name = "对接账号")]
        public string JointAccount { get; set; } = string.Empty;
        /// <summary>
        /// 对接密码
        /// </summary>
        [Column("joint_password")]
        [CompareDiffAttribute(Name = "对接密码")]
        public string JointPassword { get; set; } = string.Empty;
        /// <summary>
        /// 是否铺货 0：否 1：是
        /// </summary>
        [Column("is_distribute")]
        [CompareDiffAttribute(Name = "是否铺货")]
        public sbyte IsDistribute { get; set; }
        /// <summary>
        /// 轮保负责人
        /// </summary>
        [Column("lunbao_responsible_person")]
        [CompareDiffAttribute(Name = "轮保负责人")]
        public string LunbaoResponsiblePerson { get; set; } = string.Empty;
        /// <summary>
        /// 轮保负责人电话
        /// </summary>
        [Column("lunbao_responsible_person_tel")]
        [CompareDiffAttribute(Name = "轮保负责人电话")]
        public string LunbaoResponsiblePersonTel { get; set; } = string.Empty;
        /// <summary>
        /// 美容负责人
        /// </summary>
        [Column("meirong_responsible_person")]
        [CompareDiffAttribute(Name = "美容负责人")]
        public string MeirongResponsiblePerson { get; set; } = string.Empty;
        /// <summary>
        /// 美容负责人电话
        /// </summary>
        [Column("meirong_responsible_person_tel")]
        [CompareDiffAttribute(Name = "美容负责人电话")]
        public string MeirongResponsiblePersonTel { get; set; } = string.Empty;
        /// <summary>
        /// 暂停营业 开始时间
        /// </summary>
        [Column("suspend_start_date_time")]
        [CompareDiffAttribute(Name = "暂停营业 开始时间")]
        public DateTime SuspendStartDateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 暂停营业 结束时间
        /// </summary>
        [Column("suspend_end_date_time")]
        [CompareDiffAttribute(Name = "暂停营业 结束时间")]
        public DateTime SuspendEndDateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 是否需要安装确认码
        /// </summary>
        [Column("is_check_install_code")]
        [CompareDiffAttribute(Name = "是否需要安装确认码")]
        public int IsCheckInstallCode { get; set; }
        /// <summary>
        /// 是否创建老板账户 0否 1是
        /// </summary>
        [Column("is_create_account")]
        public sbyte IsCreateAccount { get; set; }
        /// <summary>
        /// 是否发送短信通知 0否 1是
        /// </summary>
        [Column("is_send_message")]
        public sbyte IsSendMessage { get; set; }
        /// <summary>
        /// 是否删除 0未删除 1已删除
        /// </summary>
        [Column("is_deleted")]
        [CompareDiffAttribute(Name = "是否删除")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        [CompareDiffAttribute(Name = "创建人")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [CompareDiffAttribute(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        [CompareDiffAttribute(Name = "修改人")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        [CompareDiffAttribute(Name = "修改时间")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);

    }
}
