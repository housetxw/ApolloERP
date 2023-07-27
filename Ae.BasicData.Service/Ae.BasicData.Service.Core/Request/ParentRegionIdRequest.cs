using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BasicData.Service.Core.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ParentRegionIdRequest
    {
        /// <summary>
        /// regionId
        /// </summary>
        [Required(ErrorMessage = "RegionId不能为空")]
        public string RegionId { get; set; }
    }
}
