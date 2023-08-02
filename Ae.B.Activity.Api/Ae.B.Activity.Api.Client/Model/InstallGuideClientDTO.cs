using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Model
{
    public class InstallGuideClientDTO
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

        public string CategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
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
        /// 五级车型值
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;
        /// <summary>
        /// 上架状态 0：未上架  1：已上架
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime OnlineTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 是否删除 0否 1是
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
        
        public List<InstallGuideFileClientDTO> FileList { get; set; } = new List<InstallGuideFileClientDTO>();
    }
}