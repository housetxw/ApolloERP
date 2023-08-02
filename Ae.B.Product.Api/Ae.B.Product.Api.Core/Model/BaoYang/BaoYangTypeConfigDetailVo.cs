using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
{
    /// <summary>
    /// BaoYangTypeConfigDetailVo
    /// </summary>
    public class BaoYangTypeConfigDetailVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 保养类型BaoYangType
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 产品类目
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 最小商品类目 ,分割
        /// </summary>
        public string ChildCategories { get; set; }

        /// <summary>
        /// 组别
        /// </summary>
        public string TypeGroup { get; set; }

        /// <summary>
        /// 组别显示
        /// </summary>
        public string TypeGroupDisplay
        {
            get
            {
                string display = string.Empty;
                if (TypeGroup == "Accessory")
                {
                    display = "油液";
                }
                else if (TypeGroup == "Part")
                {
                    display = "配件";
                }
                else if (TypeGroup == "Package")
                {
                    display = "套餐";
                }
                else if (TypeGroup == "Maintainence")
                {
                    display = "维保";
                }

                return display;
            }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
