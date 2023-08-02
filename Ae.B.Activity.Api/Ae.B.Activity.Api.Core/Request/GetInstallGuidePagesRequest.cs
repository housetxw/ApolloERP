using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Core.Request
{
    public class GetInstallGuidePagesRequest:BasePageRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 分类ID
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 车系
        /// </summary>
        public string VehicleSeries { get; set; } = string.Empty;
        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; } = string.Empty;
        /// <summary>
        /// 年份
        /// </summary>
        public string Nian { get; set; } = string.Empty;
        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; } = string.Empty;

        /// <summary>
        /// 上架状态 0：未上架  1：已上架 2:所有
        /// </summary>
        public sbyte Status { get; set; } = 2;
    }
}
