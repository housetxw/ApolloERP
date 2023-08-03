using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Arrival
{
    /// <summary>
    /// 项目Vo
    /// </summary>
    public class ProjectVo
    {
        /// <summary>
        /// 项目的Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 项目Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }


    }
}
