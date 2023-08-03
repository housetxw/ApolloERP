using Ae.Order.Service.Core.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;


namespace Ae.Order.Service.Core.Response.Order
{
    public class GetOrdersByUserIdResponse : OrderDTO
    {
        public OrderCarDTO OrderCar { get; set; }
        public List<OrderProductDTO> OrderProductList {get;set;}
    }
}
