using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request.BOSS
{
    /// <summary>
    /// 加盟列表Request
    /// </summary>
    public class JoinInListRequest : BasePageRequest
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 省id
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 区id
        /// </summary>
        public string DistrictId { get; set; }
    }
}
