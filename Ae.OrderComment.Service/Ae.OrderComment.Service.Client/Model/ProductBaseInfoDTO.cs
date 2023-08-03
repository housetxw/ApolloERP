using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Model
{
    /// <summary>
    /// 商品主表信息
    /// </summary>

    public class ProductBaseInfoDTO
    {
        #region  基本信息
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
        public string ShopNumber { get; set; } = string.Empty;
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
        #endregion
    }
}
