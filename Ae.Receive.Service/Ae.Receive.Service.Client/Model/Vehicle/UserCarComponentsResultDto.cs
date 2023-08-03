using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Model.Vehicle
{
    public class UserCarComponentsResultDto
    {
        /// <summary>
        /// 检查Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 配置项
        /// </summary>
        public List<long> PropertyIds { get; set; }
    }
}
