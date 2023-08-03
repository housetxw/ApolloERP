﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.ShopCost
{
    public class ShopCostVO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 费用日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 费用类别
        /// </summary>
        public long CostType { get; set; }
        /// <summary>
        /// 费用类型中文
        /// </summary>
        public string CostTypeLabel { get; set; }
        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public int InvoiceType { get; set; }
        /// <summary>
        /// 发票类型中文
        /// </summary>
        public int InvoiceTypeLabel { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string update_by { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime update_time { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }
    }
}
