﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class MyCarInfoVO
    {
        /// <summary>
        /// 车ID
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车系  
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }
        /// <summary>
        /// 车标
        /// </summary>
        public string CarLogo { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNO { get; set; }
        /// <summary>
        /// VIN码
        /// </summary>
        public string VinNO { get; set; }
    }
}
