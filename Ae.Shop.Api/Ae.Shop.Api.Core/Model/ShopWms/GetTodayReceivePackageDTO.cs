using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class GetTodayReceivePackageDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 签收记录id
        /// </summary>
        public long SignId { get; set; }
        /// <summary>
        /// 关联单号
        /// </summary>
        public string RelationNo { get; set; } = string.Empty;
        /// <summary>
        /// 物流单号
        /// </summary>
        public string PackageNo { get; set; } = string.Empty;
        /// <summary>
        /// 签收用户
        /// </summary>
        public string SignUser { get; set; } = string.Empty;
        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime SignTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 门店id
        /// </summary>
        public long ShopId { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// 是否展示清点按钮
        /// </summary>
        public bool IsShow { get; set; }
    }
}
