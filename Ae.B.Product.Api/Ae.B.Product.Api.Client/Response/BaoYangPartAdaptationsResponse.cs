using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Response
{
    public class BaoYangPartAdaptationsResponse
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 配件配置数据
        /// </summary>
        public List<BaoYangPartsAdaptationDetail> PartsAdaptation { get; set; }

        /// <summary>
        /// 特殊配件配置数据
        /// </summary>
        public List<BaoYangPartsSpecialAdaptation> PartsSpecialAdaptations { get; set; }
    }

    public class BaoYangPartsAdaptationDetail
    {
        /// <summary>
        /// 配件名
        /// </summary>
        public string PartDisplayName { get; set; }

        /// <summary>
        /// Oe号配置数据
        /// </summary>
        public List<BaoYangPartsOeCodeAdaptationDetail> OeCodeAdaptationDetails { get; set; }
    }

    public class BaoYangPartsOeCodeAdaptationDetail
    {
        /// <summary>
        /// Oe号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 配件号配置数据
        /// </summary>
        public List<BaoYangPartsPartCodeAdaptationDetail> PartCodeAdaptation { get; set; }
    }

    public class BaoYangPartsPartCodeAdaptationDetail
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标准配件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsValidation { get; set; }

        /// <summary>
        /// 操作类型  Insert  Update  Delete
        /// </summary>
        public string AuditType { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public List<BaoYangPartsProduct> PartsProducts { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }

    public class BaoYangPartsProduct
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductPid { get; set; }

        /// <summary>
        /// 上下架
        /// </summary>
        public bool IsOnSale { get; set; }

        /// <summary>
        /// 是否标缺
        /// </summary>
        public bool IsStockOut { get; set; }
    }

    public class BaoYangPartsSpecialAdaptation
    {
        /// <summary>
        /// 配件名
        /// </summary>
        public string PartDisplayName { get; set; }

        /// <summary>
        /// 配件细分
        /// </summary>
        public List<SpecialAdaptationPart> SpecialAdaptationParts { get; set; }
    }

    public class SpecialAdaptationPart
    {
        /// <summary>
        /// 配件类型细分
        /// </summary>
        public string PartType { get; set; }

        /// <summary>
        /// 特殊配件适配
        /// </summary>
        public List<BaoYangPartsPartCodeAdaptationDetail> PartCodeAdaptation { get; set; }
    }
}
