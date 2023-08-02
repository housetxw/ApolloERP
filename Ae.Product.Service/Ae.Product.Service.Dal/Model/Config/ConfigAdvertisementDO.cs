using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Config
{
    /// <summary>
    /// 广告活动页配置
    /// </summary>
    [Table("config_advertisement")]
    public class ConfigAdvertisementDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 活动code
        /// </summary>
        [Column("code")]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 活动标题
        /// </summary>
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 展示图url
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 详图url
        /// </summary>
        [Column("begin_image_url")]
        public string BeginImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮图url
        /// </summary>
        [Column("btn_image_url")]
        public string BtnImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮图2url
        /// </summary>
        [Column("btn2_image_url")]
        public string Btn2ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮图3url
        /// </summary>
        [Column("btn3_image_url")]
        public string Btn3ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 尾图url
        /// </summary>
        [Column("end_image_url")]
        public string EndImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 跳转路由url
        /// </summary>
        [Column("route_url")]
        public string RouteUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮跳转路由url
        /// </summary>
        [Column("goto_url")]
        public string GotoUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮跳转路由2url
        /// </summary>
        [Column("goto2_url")]
        public string Goto2Url { get; set; } = string.Empty;

        /// <summary>
        /// 按钮跳转路由3url
        /// </summary>
        [Column("goto3_url")]
        public string Goto3Url { get; set; } = string.Empty;

        /// <summary>
        /// 类型：区分Top头部/Bottom底部…
        /// </summary>
        [Column("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 活动开始时间
        /// </summary>
        [Column("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        [Column("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 终端类型：ShopApp(B端APP)/CbJApplet(B端小程序)/YcJApplet(C端小程序)/QpJApple(其他小程序)
        /// </summary>
        [Column("terminal_type")]
        public string TerminalType { get; set; } = string.Empty;
        /// <summary>
        /// 上下架状态
        /// </summary>
        [Column("is_online")]
        public sbyte IsOnline { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// 扩展id
        /// </summary>
        [Column("extend_id")]
        public string ExtendId { get; set; }

        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
