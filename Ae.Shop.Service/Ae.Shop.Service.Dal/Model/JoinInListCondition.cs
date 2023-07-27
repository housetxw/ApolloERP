using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class JoinInListCondition
    {

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;

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
        public int ProvinceId { get; set; }

        /// <summary>
        /// 市id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 区id
        /// </summary>
        public int DistrictId { get; set; }
    }
}
