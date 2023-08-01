using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 防冻液冰点配置
    /// </summary>
    public class AntifreezeSettingModel
    {
        /// <summary>
        /// 冰点(-30)
        /// </summary>
        public string FreezingPoint { get; set; }

        /// <summary>
        /// 品牌(道达尔/TOTAL)
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 省级ID集合
        /// </summary>
        public List<string> ProvinceIds { get; set; }
    }
}
