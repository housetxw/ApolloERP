using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Request
{
    /// <summary>
    /// InstallServiceByProductClientRequest
    /// </summary>
    public class InstallServiceByProductClientRequest
    {
        /// <summary>
        /// 产品
        /// </summary>
        public List<InstallProductClientRequest> Products { get; set; }
    }

    /// <summary>
    /// InstallProductRequest
    /// </summary>
    public class InstallProductClientRequest
    {
        /// <summary>
        /// 产品Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 安装类型
        /// </summary>
        public string InstallType { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
    }
}
