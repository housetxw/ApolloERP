using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.BasicData.Service.Core.Response;

namespace Ae.BasicData.Service.Core.Request
{
    public class RegionChinaReqDTO { }

    public class RegionChinaReqByRegionIdDTO
    {
        /// <summary>
        /// RG内部使用唯一标识； 一旦初始化，便不会更改；
        /// RegionId只有一位时(省、自治区、直辖市)，是 '0'；其他级别的行政单位RegionId是6位；
        /// </summary>
        [Required(ErrorMessage = "RegionId不能为空")]
        [RegularExpression(@"^[0-9\s]{1,6}$", ErrorMessage = "请输入正确的RegionId")]
        public string RegionId { get; set; }
    }

    public class RegionChinaReqByLevelDTO
    {
        /// <summary>
        /// 地区级别:
        /// 1-省、自治区、直辖市；
        /// 2-地级市、地区、自治州、盟；
        /// 3-市辖区、县级市、县；
        /// </summary>
        [Required(ErrorMessage = "Region Level不能为空")]
        [Range(1, 3, ErrorMessage = "请选择正确的Region Level")]
        public RegionChinaType Level { get; set; }
    }


}
