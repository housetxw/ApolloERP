using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.BaoYang
{
    /// <summary>
    /// 保养项目
    /// </summary>
    public class BaoYangPackageDto
    {
        /// <summary>
        /// 保养项目类型：xby,dby,ys,scy
        /// </summary>
        public string PackageType { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string ZhName { get; set; }

        /// <summary>
        /// 项目推荐数据
        /// </summary>
        public string SuggestTip { get; set; }

        /// <summary>
        /// 项目简单描述
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// 项目描述链接
        /// </summary>
        public string DescriptionLink { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsDefaultExpand { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public List<TagInfoDto> Tags { get; set; }

        /// <summary>
        /// 组别_互斥判别(EG:大小保养互斥)
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 项目内容
        /// </summary>
        public List<BaoYangPackageItemDto> Items { get; set; }

        /// <summary>
        /// 如果没有产品也没有属性，即不适配，则返回不适配的原因
        /// </summary>
        public string InAdaptReason { get; set; }
    }
}
