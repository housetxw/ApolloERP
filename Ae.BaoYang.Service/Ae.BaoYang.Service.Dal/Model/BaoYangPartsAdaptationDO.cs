using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    /// <summary>
    /// 保养配件配置
    /// </summary>
    public class BaoYangPartsAdaptationDO
    {
        public int Id { get; set; }

        public string Tid { get; set; }

        public string PartName { get; set; }

        public string OePartCode { get; set; }

        public string PartCode { get; set; }

        public string Brand { get; set; }

        public string Pid { get; set; }

        public bool OnSale { get; set; }

        /// <summary>
        /// 标缺
        /// </summary>
        public bool StockOut { get; set; }
    }
}
