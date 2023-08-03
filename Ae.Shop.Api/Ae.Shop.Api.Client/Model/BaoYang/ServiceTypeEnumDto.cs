using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model.BaoYang
{
    public class ServiceTypeEnumDto
    {
        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 显示中文
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int Id { get; set; }
    }
}
