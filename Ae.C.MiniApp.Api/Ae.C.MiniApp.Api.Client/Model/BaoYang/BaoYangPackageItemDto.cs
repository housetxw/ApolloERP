using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.BaoYang
{
    public class BaoYangPackageItemDto
    {
        /// <summary>
        /// 保养类型
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string ZhName { get; set; }

        /// <summary>
        /// 机油参考用量,仅在机油中有用,配选机油总量超过参考量时提示(EG:4.0L)
        /// </summary>
        public string DataTip { get; set; }

        /// <summary>
        /// 结果类型，产品或者需要选择属性
        /// </summary>
        public string ResultType { get; set; }

        /// <summary>
        /// 适配的产品
        /// </summary>
        public List<BaoYangPackageProductDto> Products { get; set; }

        /// <summary>
        /// 需要选择的五级属性
        /// </summary>
        public PropertySelectDto Property { get; set; }

        /// <summary>
        /// 如果没有产品也没有属性，即不适配，则返回不适配的原因
        /// </summary>
        public string InAdaptReason { get; set; }

        /// <summary>
        /// 组别_替换的时候一起替换EG： zys  yys
        /// </summary>
        public string GroupName { get; set; }
    }
}
