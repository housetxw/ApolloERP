using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    /// <summary>
    /// 检查大类
    /// </summary>
    public enum ReceiveCheckClassfyEnum
    {
        /// <summary>
        /// 提交报告
        /// </summary>
        [Description("提交报告")]
        Submit=0,

        /// <summary>
        /// 客户描述
        /// </summary>
        [Description("客户描述")]
        CustomerDescription = 1,

        /// <summary>
        /// 仪表盘
        /// </summary>
        [Description("仪表盘")]
        Dashboard =2,

        /// <summary>
        /// 内饰
        /// </summary>
        [Description("内饰")]
        Inlook =3,

        /// <summary>
        /// 外观
        /// </summary>
        [Description("外观")]
        Outlook =4,

        /// <summary>
        /// 附加
        /// </summary>
        [Description("附加")]
        Other =5,

        /// <summary>
        /// 电池组
        /// </summary>
        [Description("电池组")]
        BatteryPack =6,

        /// <summary>
        /// 高压线束
        /// </summary>
        [Description("高压线束")]
        HighVoltageWiring =7,

        /// <summary>
        /// 充电系统
        /// </summary>
        [Description("充电系统")]
        ChargingSystem =8,

        /// <summary>
        /// 驾驶室
        /// </summary>
        [Description("驾驶室")]
        DrivingCab =9,

        /// <summary>
        /// 灯光
        /// </summary>
        [Description("灯光")]
        Lamplight =10,

        /// <summary>
        /// 发动机舱
        /// </summary>
        [Description("发动机舱")]
        EngineRoom =12,

        /// <summary>
        /// 轮胎
        /// </summary>
        [Description("轮胎")]
        Tire =13,

        /// <summary>
        /// 制动盘/片
        /// </summary>
        [Description("制动盘/片")]
        BrakeDisc =14,

        /// <summary>
        /// 技师签字
        /// </summary>
        [Description("技师签字")]
        TechnicianSignature =15,

        /// <summary>
        /// 客户签字
        /// </summary>
        [Description("客户签字")]
        CustomerSignature =16,

        /// <summary>
        /// 上门检查
        /// </summary>
        [Description("上门检查")]
        DropInCheck = 17,

        /// <summary>
        /// 纸质报告
        /// </summary>
        [Description("纸质报告")]
        DropInPageCheck = 18,
        /// <summary>
        /// 质检
        /// </summary>
        [Description("质检")]
        ZhiJian =19,
        /// <summary>
        /// 质检
        /// </summary>
        [Description("质检签字")]
        ZhiJianSignature =20
    }
}
