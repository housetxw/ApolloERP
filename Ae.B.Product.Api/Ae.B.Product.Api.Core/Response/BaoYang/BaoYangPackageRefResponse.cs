using Ae.B.Product.Api.Core.Model.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.Product.Api.Core.Response.Vehicle;

namespace Ae.B.Product.Api.Core.Response.BaoYang
{
    /// <summary>
    /// 
    /// </summary>
    public class BaoYangPackageRefResponse
    {
        /// <summary>
        /// 套餐信息
        /// </summary>
        public List<BaoYangPackageRefVo> Packages { get; set; } = new List<BaoYangPackageRefVo>();

        /// <summary>
        /// Tid信息
        /// </summary>
        public List<VehicleTidResponse> Vehicles { get; set; } = new List<VehicleTidResponse>();
    }
}
