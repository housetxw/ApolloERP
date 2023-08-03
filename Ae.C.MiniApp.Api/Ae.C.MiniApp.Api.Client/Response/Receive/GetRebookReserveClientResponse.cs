﻿using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class GetRebookReserveClientResponse
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<ReserveRebookProductDTO> Products { get; set; }
        /// <summary>
        /// 车型信息
        /// </summary>
        public MyCarInfoDTO CarInfo { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 预约项目Code
        /// </summary>
        public string ServiceCode { get; set; }
        /// <summary>
        /// 服务大类名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 门店电话
        /// </summary>
        public string Phone { get; set; } = string.Empty;
    }
}
