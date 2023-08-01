using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model.Extend
{
    public class BaoYangPartProductDO
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 配件名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// OE号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 产品品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string Pid { get; set; }
    }
}
