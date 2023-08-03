using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Response.Product
{
    public class ProductSearchClientResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ProductAllInfoDto> Items { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }
    }

    public class ProductAllInfoDto
    {
        #region 基本信息

        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 类别（ 2子产品，4 父产品）
        /// </summary>
        public int ClassType { get; set; }

        /// <summary>
        ///  父产品的Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///  税率
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// 基准价格
        /// </summary>
        public decimal StandardPrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        ///  单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        ///  长
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        ///  宽
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        ///  高
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        ///  产品重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        ///  颜色
        /// </summary>
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// 主图连接
        /// </summary>
        public string Image1 { get; set; } = string.Empty;

        /// <summary>
        /// 图片链接2
        /// </summary>
        public string Image2 { get; set; } = string.Empty;

        /// <summary>
        /// 图片链接3
        /// </summary>
        public string Image3 { get; set; } = string.Empty;

        /// <summary>
        /// 图片链接4
        /// </summary>
        public string Image4 { get; set; } = string.Empty;

        /// <summary>
        /// 图片链接5
        /// </summary>
        public string Image5 { get; set; } = string.Empty;

        /// <summary>
        /// 通用 - 标准配件号
        /// </summary>
        public string PartCode { get; set; } = string.Empty;

        /// <summary>
        /// 通用 - 配件编号 （用于保养品）
        /// </summary>
        public string PartNo { get; set; } = string.Empty;

        /// <summary>
        ///  推荐位置（填写>=1的整数，0为不推荐），用于搜索结果
        /// </summary>
        public int ProductRefer { get; set; }

        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品
        /// </summary>
        public sbyte ProductAttribute { get; set; }

        /// <summary>
        ///  上下架
        /// </summary>
        public sbyte OnSale { get; set; }

        /// <summary>
        /// 是否缺货
        /// </summary>
        public sbyte Stockout { get; set; }

        /// <summary>
        ///  是否显示  -  0 否 1 是 用于控制产品是否在搜索及产品列表页显示。
        /// </summary>
        public sbyte IsShow { get; set; }

        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 是否门店外采商品 1 是 0 否 默认 0
        /// </summary>
        public sbyte IsStoreoutside { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        ///  是否零售 0 否 1 是
        /// </summary>
        public sbyte IsRetail { get; set; }

        /// <summary>
        ///  是否有保险 0 否 1 是
        /// </summary>
        public sbyte IsInsurance { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 评分
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        ///  销量
        /// </summary>
        public int Sales { get; set; }

        /// <summary>
        /// 好评率
        /// </summary>
        public decimal FavorableRate { get; set; }

        /// <summary>
        /// 广告促销语
        /// </summary>
        public string Advertisement { get; set; } = string.Empty;

        /// <summary>
        /// 批发价
        /// </summary>
        public decimal WholesalePrice { get; set; }

        /// <summary>
        /// 返现价
        /// </summary>
        public decimal ReturnCash { get; set; }

        /// <summary>
        /// 门店结算价
        /// </summary>
        public decimal SettlementPrice { get; set; }

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

        #endregion

        #region 类目信息

        /// <summary>
        /// 一级类目Id
        /// </summary>
        public int MainCategoryId { get; set; }

        /// <summary>
        /// 顶级类目Code
        /// </summary>
        public string MainCategoryCode { get; set; }

        /// <summary>
        /// 顶级类目ShortCode
        /// </summary>
        public string MainCategoryShortCode { get; set; }

        /// <summary>
        /// 顶级类目名称
        /// </summary>
        public string MainCategoryName { get; set; }


        /// <summary>
        /// 二级类目Id
        /// </summary>
        public int SecondCategoryId { get; set; }

        /// <summary>
        /// 关联类目Code
        /// </summary>
        public string SecondCategoryCode { get; set; }

        /// <summary>
        /// 关联类目ShortCode
        /// </summary>
        public string SecondCategoryShortCode { get; set; }

        /// <summary>
        /// 关联类目名称
        /// </summary>
        public string SecondCategoryName { get; set; }


        /// <summary>
        /// 三级类目Id
        /// </summary>
        public int ChildCategoryId { get; set; }

        /// <summary>
        /// 关联类目Code
        /// </summary>
        public string ChildCategoryCode { get; set; }

        /// <summary>
        /// 关联类目ShortCode
        /// </summary>
        public string ChildCategoryShortCode { get; set; }

        /// <summary>
        /// 关联类目名称
        /// </summary>
        public string ChildCategoryName { get; set; }

        #endregion
    }

    public class ProductInstallserviceDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 产品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 服务id
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务价格
        /// </summary>
        public decimal ServicePrice { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 包安装
        /// </summary>
        public sbyte FreeInstall { get; set; }

        /// <summary>
        /// 服务数量
        /// </summary>
        public sbyte ChangeNum { get; set; }

        /// <summary>
        /// 是否 删除 0 否  1是  
        /// </summary>
        public sbyte IsDeleted { get; set; } = 0;
    }

    /// <summary>
    /// 商品属性
    /// </summary>
    public class ProductAttributevalueDto
    {

        /// <summary>
        /// 从表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 属性名id
        /// </summary>
        public int AttributeNameId { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// 商品属性值
        /// </summary>
        public string AttributeValue { get; set; }

        /// <summary>
        /// 是否 删除 0 否  1是  
        /// </summary>
        public sbyte IsDeleted { get; set; } = 0;
    }

    /// <summary>
    /// 套餐明细
    /// </summary>
    public class ProductPackageDetailDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; } = string.Empty;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 项目类型 1 产品PId 2 配件 （机油滤芯，燃油滤芯，汽油滤芯） 
        /// </summary>
        public sbyte ProjectType { get; set; }

        /// <summary>
        /// 项目品牌
        /// </summary>
        public List<string> ProjectBrands { get; set; }

        /// <summary>
        /// 基准价格
        /// </summary>
        public decimal StandardPrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 产品数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 能否替换产品
        /// </summary>
        public sbyte ReplaceProduct { get; set; }

        /// <summary>
        /// 替换产品类目 逗号分隔,
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 替换外采产品类目
        /// </summary>
        public string ShopCategoryId { get; set; }

        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public sbyte IsDeleted { get; set; }
    }
}
