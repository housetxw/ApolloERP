using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 安装方式
    /// </summary>
    public class InstallTypeModel
    {
        /// <summary>
        /// 安装类型Type(EG:QYS)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 安装类型名称(EG:雨刷套装)
        /// </summary>
        public string ZhName { get; set; }
        /// <summary>
        /// 包含的类型Code
        /// </summary>
        public List<string> ContainedByTypes { get; set; }
        /// <summary>
        /// 产品是否能删除
        /// </summary>
        public bool NeedAll { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 显示信息
        /// </summary>
        public List<InstallTypeText> Infos { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InstallTypeText
    {
        /// <summary>
        /// 图片Url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 显示文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public string Color { get; set; }
    }
}
