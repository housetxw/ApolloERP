using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
{
    /// <summary>
    /// 保养适配分类大类
    /// </summary>
    public class BaoYangCategoryVO
    {
        /// <summary>
        /// 一级分类CODE(EG:Normal)
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 一级类型名称(EG:常规保养项目)
        /// </summary>
        public string CategoryType { get; set; }

        /// <summary>
        /// 一级类型简单名(EG:基础养护)
        /// </summary>
        public string SimpleCategoryName { get; set; }

        /// <summary>
        /// 保养项目
        /// </summary>
        public List<BaoYangPackageModel> PackageItems { get; set; }
    }

    /// <summary>
    /// 保养项目
    /// </summary>
    public class BaoYangPackageModel
    {
        /// <summary>
        /// 保养项目类型：xby,dby,ys,scy
        /// </summary>
        public string PackageType { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string ZhName { get; set; }

        /// <summary>
        /// 项目推荐数据
        /// </summary>
        public string SuggestTip { get; set; }

        /// <summary>
        /// 项目简单描述
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// 项目描述链接
        /// </summary>
        public string DescriptionLink { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsDefaultExpand { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public List<TagInfo> Tags { get; set; }

        /// <summary>
        /// 组别_互斥判别(EG:大小保养互斥)
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 项目内容
        /// </summary>
        public IEnumerable<BaoYangPackageItemModel> Items { get; set; }

        /// <summary>
        /// 如果没有产品也没有属性，即不适配，则返回不适配的原因
        /// </summary>
        public string InAdaptReason { get; set; }
    }
    /// <summary>
    /// 标签
    /// </summary>
    public class TagInfo
    {
        /// <summary>
        /// 标签CODE(EG:RGRecommend)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 标签名称(EG:总部推荐)
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 标签颜色(EG:#F37C3E)
        /// </summary>
        public string TagColor { get; set; }
    }

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
        public double Price { get; set; }

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

    /// <summary>
    /// 项目内容
    /// </summary>
    public class BaoYangPackageItemModel
    {
        /// <summary>
        /// 保养类型
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string ZhName { get; set; }

        /// <summary>
        /// 机油参考用量,仅在机油中有用,配选机油总量超过参考量时提示(EG:4.0L)
        /// </summary>
        public string DataTip { get; set; }

        /// <summary>
        /// 结果类型，产品或者需要选择属性
        /// </summary>
        public string ResultType { get; set; }

        /// <summary>
        /// 适配的产品
        /// </summary>
        public List<BaoYangPackageProductModel> Products { get; set; }

        /// <summary>
        /// 需要选择的五级属性
        /// </summary>
        public PropertySelectModel Property { get; set; }

        /// <summary>
        /// 如果没有产品也没有属性，即不适配，则返回不适配的原因
        /// </summary>
        public string InAdaptReason { get; set; }

        /// <summary>
        /// 组别_替换的时候一起替换EG： zys  yys
        /// </summary>
        public string GroupName { get; set; }
    }

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
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否默认选中
        /// </summary>
        public bool IsDefaultSelect { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public double Price { get; set; }

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
        public List<TagInfo> Tags { get; set; }

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
        /// 是否可跳转到商品详情
        /// </summary>
        public string GotoProductDetail { get; set; }

        /// <summary>
        /// 只给前端用 没有任何意思
        /// </summary>
        public bool IsShowAllPackage { get; set; }
    }

    /// <summary>
    /// 需要选择的五级属性
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
