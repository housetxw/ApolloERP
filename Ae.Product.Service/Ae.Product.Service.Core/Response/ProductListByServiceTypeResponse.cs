using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.Product.Service.Core.Response
{
    /// <summary>
    /// 服务大类 商品列表
    /// </summary>
    public class ProductListByServiceTypeResponse
    {
        /// <summary>
        /// 商品
        /// </summary>
        public List<BaoYangPackageProductModel> Products { get; set; } = new List<BaoYangPackageProductModel>();

        /// <summary>
        /// 默认轮胎尺寸
        /// </summary>
        public string DefaultTireSize { get; set; }

        /// <summary>
        /// 更多轮胎尺寸
        /// </summary>
        public List<string> TireSizes { get; set; }
    }

    /// <summary>
    /// 适配结果类型
    /// </summary>
    public enum AdaptationResultType
    {
        /// <summary>
        /// 不适配
        /// </summary>
        None,
        /// <summary>
        /// 产品
        /// </summary>
        Product,
        /// <summary>
        /// 属性
        /// </summary>
        Property
    }

    /// <summary>
    /// 商品
    /// </summary>
    public class BaoYangPackageProductModel
    {
        /// <summary>
        /// 需要选择的五级属性
        /// </summary>
        public PropertySelectModel Property { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string Pid { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 铺货成本
        /// </summary>
        public decimal SettlementPrice { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// 适配数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;

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
        public bool GotoProductDetail { get; set; }

        /// <summary>
        /// 替换商品
        /// </summary>
        public bool ReplaceProduct { get; set; } = false;

        /// <summary>
        /// 套餐明细主键
        /// </summary>
        public int PackageDetailId { get; set; }

        /// <summary>
        /// 分组Id 用于选商品互斥的情况
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; } = string.Empty;

        /// <summary>
        /// 产品类型 0：自营产品   1：门店服务项目  2：门店外采
        /// </summary>
        public int ProductType { get; set; }
    }

    /// <summary>
    /// 属性选择
    /// </summary>
    public class PropertySelectModel
    {
        /// <summary>
        /// 属性类型(TID或者Property)
        /// 如果为TID,查询赋值时直接使用Values数组中的Key
        /// 如果为Property,则修改车型Properties数组中数据
        /// [{"PropertyKey":"发动机","PropertyValue":"CDD（2009/05/01之后）"}]
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 筛选Title(EG:请选择年款)
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 筛选说明(EG:您所选的车型存在多个年款，不同年款的车型，配件存在差异)
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 如果Type为Property时,对应车型Properties中PropertyKey值
        /// EG:年款
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 如果Type为Property时,Values中的KEY 对应车型Properties中PropertyValue值
        /// 如果Type为Tid是,Values中的KEY 对应车型Properties中Tid值
        /// </summary>
        public List<PropertyResult> Values { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PropertyResult
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 值(EG:2015)
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 显示(EG:2015)
        /// </summary>
        public string DisplayValue { get; set; }
    }
}
