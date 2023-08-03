using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetShopAssetListRequest : BasePageRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 一级类别编号
        /// </summary>
        public string FirstTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 二级类别编号
        /// </summary>
        public string SecondTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 资产名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 资产编码
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 增加方式（0未设置 1购入 2转入）
        /// </summary>
        public sbyte AddMethod { get; set; }
        /// <summary>
        /// 使用状态（0未设置 1使用中 2已报废 3已调拨）
        /// </summary>
        public sbyte UseStatus { get; set; }
        /// <summary>
        /// 开始使用日期起始
        /// </summary>
        public DateTime StartUseDateOrigin { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 开始使用日期截止
        /// </summary>
        public DateTime StartUseDateOff { get; set; } = new DateTime(1900, 1, 1);
    }
}
