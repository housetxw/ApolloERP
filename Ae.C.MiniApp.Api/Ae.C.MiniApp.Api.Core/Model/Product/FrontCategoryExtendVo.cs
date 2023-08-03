using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Product
{
    /// <summary>
    /// FrontCategoryExtendVo
    /// </summary>
    public class FrontCategoryExtendVo
    {
        /// <summary>
        /// PackageType
        /// </summary>
        public List<string> PackageType { get; set; }

        /// <summary>
        /// ServiceType
        /// </summary>
        public int ServiceType { get; set; }
    }
}
