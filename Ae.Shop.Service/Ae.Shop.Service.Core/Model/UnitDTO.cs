using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class UnitDTO
    {
        /// <summary>
        /// 单位id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单位类型 0：公司；1：门店；2：仓库；
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 状态 0正常 1终止 2暂停
        /// </summary>
        public int Status { get; set; }
    }

    public class UnitSelDTO
    {
        public long Value { get; set; }
        public string Label { get; set; }
        public int Type { get; set; }
    }

}
