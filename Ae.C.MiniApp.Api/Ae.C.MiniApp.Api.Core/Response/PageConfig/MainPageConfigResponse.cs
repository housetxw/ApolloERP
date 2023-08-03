using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.PageConfig
{
    /// <summary>
    /// 首页功能配置
    /// </summary>
    public class MainPageConfigResponse
    {
        /// <summary>
        /// 头部广告位
        /// </summary>
        public List<AdvertisingModel> TopAdvertising { get; set; }

        /// <summary>
        /// 功能模块
        /// </summary>
        public List<ModuleBlock> Modules { get; set; }

        /// <summary>
        /// 超值活动
        /// </summary>
        public List<AdvertisingModel> ProminentActive { get; set; }
    }

    /// <summary>
    /// 广告位
    /// </summary>
    public class AdvertisingModel
    {
        /// <summary>
        /// 活动页面ID：Code
        /// </summary>
        public string AdvertisingCode { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 详图url
        /// </summary>
        public string BeginImageUrl { get; set; } 

        /// <summary>
        /// 按钮图url
        /// </summary>
        public string BtnImageUrl { get; set; }

        /// <summary>
        /// 按钮图2url
        /// </summary>
        public string Btn2ImageUrl { get; set; }

        /// <summary>
        /// 按钮图3url
        /// </summary>
        public string Btn3ImageUrl { get; set; }

        /// <summary>
        /// 尾图url
        /// </summary>
        public string EndImageUrl { get; set; } 

        /// <summary>
        /// 路由地址
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 按钮跳转路由url
        /// </summary>
        public string GotoUrl { get; set; }

        /// <summary>
        /// 按钮跳转路由2url
        /// </summary>
        public string Goto2Url { get; set; }

        /// <summary>
        /// 按钮跳转路由3url
        /// </summary>
        public string Goto3Url { get; set; }

        /// <summary>
        /// 扩展参数
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 类型：Top 顶部显示，Bottom 中间显示
        /// </summary>
        public string Type { get; set; }
    }

    /// <summary>
    /// 功能模块
    /// </summary>
    public class ModuleBlock
    {
        /// <summary>
        /// 类目
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Icon图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 二级标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 默认展开项
        /// </summary>
        public List<string> PackageType { get; set; }

        /// <summary>
        /// 服务类型 0到店服务   1上门服务
        /// </summary>
        public int ServiceType { get; set; }
    }
}
