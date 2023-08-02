using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
{
    /// <summary>
    /// BaoYangPackageConfigVo
    /// </summary>
    public class BaoYangPackageConfigVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// PackageType
        /// </summary>
        public string PackageType { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 建议提示
        /// </summary>
        public string SuggestTip { get; set; }

        /// <summary>
        /// 简要描述
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// 描述链接url
        /// </summary>
        public string DescriptionLink { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string DetailDescription { get; set; }

        /// <summary>
        /// 适配展示商品类型：0套餐  1单品  2套餐&单品
        /// </summary>
        public int ProductType { get; set; }

        /// <summary>
        /// 适配展示商品类型 - 展示
        /// </summary>
        public string ProductTypeName
        {
            get
            {
                string name = string.Empty;
                if (ProductType == 0)
                {
                    name = "套餐";
                }
                else if (ProductType == 1)
                {
                    name = "单品";
                }
                else if (ProductType == 2)
                {
                    name = "套餐|单品";
                }

                return name;
            }
        }

        /// <summary>
        /// 图片logo
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 排序显示
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// 服务pid
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// BaoYangType
        /// </summary>
        public List<string> BaoYangType { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        public string CategoryAlias { get; set; }

        /// <summary>
        /// 一级分类-名称
        /// </summary>
        public string CategoryName { get; set; }
    }
}
