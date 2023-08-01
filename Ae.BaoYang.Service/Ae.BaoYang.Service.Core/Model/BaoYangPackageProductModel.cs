using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 保养产品model
    /// </summary>
    public class BaoYangPackageProductModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        /// 适配数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfo> Tags { get; set; } = new List<TagInfo>();

        /// <summary>
        /// 标缺
        /// </summary>
        public bool StockOut { get; set; }

        /// <summary>
        /// 是否原厂
        /// </summary>
        public bool IsOriginal { get; set; }

        /// <summary>
        /// 好评率
        /// </summary>
        public decimal FeedbackRate { get; set; }

        /// <summary>
        /// 是否套餐产品
        /// </summary>
        public bool IsPackageProduct { get; set; }

        /// <summary>
        /// 套餐子产品
        /// </summary>
        public IEnumerable<BaoYangPackageProductModel> ChildProducts { get; set; }

        /// <summary>
        /// 是否关注
        /// </summary>
        public bool IsAttention { get; set; }

        /// <summary>
        /// 是否默认选中
        /// </summary>
        public bool IsDefaultSelect { get; set; }

        /// <summary>
        /// 是否可跳转到商品详情
        /// </summary>
        public bool GotoProductDetail
        {
            get
            {
                if (this.IsPackageProduct)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.Pid))
                {
                    return false;
                }

                if (this.Pid.EndsWith("-FU"))
                {
                    return false;
                }

                return true;
            }

        }

        /// <summary>
        /// 替换商品
        /// </summary>
        public bool ReplaceProduct { get; set; } = false;

        /// <summary>
        /// 套餐明细主键
        /// </summary>
        public int PackageDetailId { get; set; }
    }
}
