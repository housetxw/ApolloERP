using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class LocationRequest
    {
        /// <summary>
        /// 经度
        /// </summary>
        [Required(ErrorMessage = "经度不能为空")]
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        [Required(ErrorMessage = "维度不能为空")]
        public double Latitude { get; set; }
    }
}
