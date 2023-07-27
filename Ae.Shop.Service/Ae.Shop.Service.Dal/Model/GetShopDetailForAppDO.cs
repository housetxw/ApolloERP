using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class GetShopDetailForAppDO
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
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Head { get; set; } = string.Empty;
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string HeadPhone { get; set; } = string.Empty;
        /// <summary>
        /// 负责人邮箱
        /// </summary>
        public string HeadEmail { get; set; } = string.Empty;
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 快速排队小程序码
        /// </summary>
        public string AppletCode { get; set; }
        /// <summary>
        /// 门店小程序码
        /// </summary>        
        public string ShopAppletCode { get; set; } = string.Empty;
        /// <summary>
        /// 总评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 总订单数量
        /// </summary>
        public int TotalOrder { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartWorkTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndWorkTime { get; set; }
        /// <summary>
        /// 休息室
        /// </summary>
        public bool IsLoungeRoom { get; set; }
        /// <summary>
        /// 卫生间
        /// </summary>
        public bool IsRestRoom { get; set; }

        /// <summary>
        /// 免费wifi
        /// </summary>
        public bool IsFreeWiFi { get; set; }
        /// <summary>
        /// 汽车故障诊断仪
        /// </summary>
        public bool CarFaultDiagnosticTool { get; set; }
        /// <summary>
        /// 柱式举升机
        /// </summary>
        public bool IsPostLift { get; set; }

        public string TagName { get; set; }
        /// <summary>
        /// 门店老板姓名
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板电话
        /// </summary>
        public string OwnerPhone { get; set; } = string.Empty;
    }
}
