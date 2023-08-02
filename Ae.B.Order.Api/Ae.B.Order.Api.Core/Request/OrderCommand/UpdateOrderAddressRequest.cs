using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderCommand
{
    public class UpdateOrderAddressRequest
    {
        /// <summary>
        /// 订单No
        /// </summary>
        public string OrderNo { get; set; }

        public string ReceiverName { get; set; } = string.Empty;

        public string ReceiverPhone { get; set; } = string.Empty;

        public string ProvinceId { get; set; } = string.Empty;

        public string Province { get; set; } = string.Empty;

        public string CityId { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string DistrictId { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public string DetailAddress { get; set; } = string.Empty;


        public string UpdateBy { get; set; } = string.Empty;

    }
}
