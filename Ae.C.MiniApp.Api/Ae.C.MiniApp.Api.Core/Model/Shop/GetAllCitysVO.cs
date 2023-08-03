﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class GetAllCitysVO
    {
        /// <summary>
        /// 首字母
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<CityInfoVO> Citys { get; set; }
    }

    public class CityInfoVO
    {
        /// <summary>
        /// 城市ID
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 省ID
        /// </summary>
        public string ProvinceId { get; set; }
    }
}
