using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Core.Model
{
    public class AppVersionDTO
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public long Code { get; set; }

        /// <summary>
        /// 版本号名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 版本URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 版本配置
        /// </summary>
        public string Config { get; set; }

        /// <summary>
        /// 版本时间
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>
        /// 标识此版本，是否让灰度门店升级
        /// </summary>
        public bool IsForce { get; set; }

        /// <summary>
        /// 标识此版本，正式版是否发布
        /// </summary>
        public bool IsRelease { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

}
