﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    public class InsuranceCompanyDTO
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 保险公司
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 保险公司图片
        /// </summary>
    
        public string Img { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
