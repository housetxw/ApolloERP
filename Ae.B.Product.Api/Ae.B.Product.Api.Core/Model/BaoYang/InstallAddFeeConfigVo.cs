﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
{
    /// <summary>
    /// InstallAddFeeConfigVo
    /// </summary>
    public class InstallAddFeeConfigVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 服务pid
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 车型最小指导价
        /// </summary>
        public string CarMinPrice { get; set; }

        /// <summary>
        /// 车型最大指导价
        /// </summary>
        public string CarMaxPrice { get; set; }

        /// <summary>
        /// 服务原价
        /// </summary>
        public string OriginalPrice { get; set; }

        /// <summary>
        /// 车型加价金额
        /// </summary>
        public string AdditionalPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
