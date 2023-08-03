using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Product
{
    public class ConfigAdvertisementDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 活动code
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 展示图url
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 详图url
        /// </summary>
        public string BeginImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮图url
        /// </summary>
        public string BtnImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮图2url
        /// </summary>
        public string Btn2ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮图3url
        /// </summary>
        public string Btn3ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 尾图url
        /// </summary>
        public string EndImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 跳转路由url
        /// </summary>
        public string RouteUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮跳转路由url
        /// </summary>
        public string GotoUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮跳转路由2url
        /// </summary>
        public string Goto2Url { get; set; } = string.Empty;

        /// <summary>
        /// 按钮跳转路由3url
        /// </summary>
        public string Goto3Url { get; set; } = string.Empty;

        /// <summary>
        /// 类型：区分Top头部/Bottom底部…
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 终端类型：ShopApp门店管家/CbJApplet门店/YcJApplet养车
        /// </summary>
        public string TerminalType { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 扩展id
        /// </summary>
        public string ExtendId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
