using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class ShopDetailVO
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 简单名称
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 总评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 总订单数量
        /// </summary>
        public int OrderTotalCount { get; set; }
        /// <summary>
        /// 距离（km）
        /// </summary>
        public decimal Distance { get; set; }

        /// <summary>
        /// 门店图片
        /// </summary>
        public string[] Imgs { get; set; }
        /// <summary>
        /// 营业时间
        /// </summary>
        public string WorkTime { get; set; }

        /// <summary>
        /// 门店技师
        /// </summary>
        public List<TechVO> Techs { get; set; }
        /// <summary>
        /// 门店服务
        /// </summary>
        public List<SimpleServiceVO> ShopServices { get; set; }
    }
}
