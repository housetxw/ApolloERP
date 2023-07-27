using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class BaseServiceDTO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 服务PID
        /// </summary>
        [Required(ErrorMessage = "服务PID不能为空")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 英文CODE
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 默认销售价
        /// </summary>
        public decimal DefaultSalePrice { get; set; }
        /// <summary>
        /// 默认成本价
        /// </summary>
        public decimal DefaultCostPrice { get; set; }
        /// <summary>
        /// 销售价格限制
        /// </summary>
        public decimal DefaultSalePriceLimit { get; set; }
        /// <summary>
        /// 成本价限制
        /// </summary>
        public decimal DefaultCostPriceLimit { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public long CategoryId { get; set; }
        /// <summary>
        /// 分类类型
        /// </summary>
        public string CategoryType { get; set; } = string.Empty;
        /// <summary>
        /// 服务备注
        /// </summary>
        public string ServiceRemark { get; set; } = string.Empty;
        /// <summary>
        /// 价格备注
        /// </summary>
        public string SalePriceRemark { get; set; } = string.Empty;
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceCharge { get; set; }
        /// <summary>
        /// 是否删除 0未删除 1已删除
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
