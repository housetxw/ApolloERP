using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Imp.Model
{
    public class PackageDescriptionConfig
    {
        public List<BaoYangPackageDescription> Packages { get; set; } = new List<BaoYangPackageDescription>();
    }

    public class BaoYangPackageDescription
    {
        public string PackageType { get; set; }

        public string DisplayName { get; set; }

        /// <summary>
        /// 项目简单描述
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// 项目推荐数据
        /// </summary>
        public string SuggestTip { get; set; }

        /// <summary>
        /// 项目详细描述
        /// </summary>
        public string DetailDescription { get; set; }

        /// <summary>
        /// 项目描述链接
        /// </summary>
        public string DescriptionLink { get; set; }

        public List<BaoYangTypeDescription> Items { get; set; }

        /// <summary>
        /// 服务pid
        /// </summary>
        public List<string> ServiceIds { get; set; }

        /// <summary>
        /// 展示商品类型 0套餐 1商品 2套餐&商品
        /// </summary>
        public int ProductType { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
    }

    public class BaoYangTypeDescription
    {
        /// <summary>
        /// 保养类型
        /// </summary>
        public string BaoYangType { get; set; }

        public string DisplayName { get; set; }

        public string CatalogName { get; set; }

        public string Group { get; set; }
    }

    public class InstallTypePackage
    {
        public string PackageType { get; set; }

        public List<InstallTypeConfig> InstallTypeConfig { get; set; }
    }

    public class InstallTypeConfig
    {
        /// <summary>
        /// 安装方式
        /// </summary>
        public string InstallType { get; set; }

        /// <summary>
        /// 显示名字
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> BaoYangType { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool NeedAll { get; set; }

        public string TextFormat { get; set; }


        public List<string> Channel { get; set; }

        /// <summary>
        /// 0：单品切换   1：套餐切换
        /// </summary>
        public int SpecialType { get; set; }
    }
}
