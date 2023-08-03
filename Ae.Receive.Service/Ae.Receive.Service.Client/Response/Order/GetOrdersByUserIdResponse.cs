using Ae.Receive.Service.Client.Model;
using Ae.Receive.Service.Client.Response.Order;
using System;
using System.Collections.Generic;
using System.Text;


namespace Ae.Receive.Service.Client.Response
{
    public class GetOrdersByUserIdResponse : OrderDTO
    {
        public OrderCarDTO OrderCar { get; set; }
        public List<OrderProductDTO> OrderProductList {get;set;}
    }
}
