using Ae.Receive.Service.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Client.Model.BaoYang;
using Ae.Receive.Service.Core.Model;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Dal.Repositorys;
using Ae.Receive.Service.Client.Model.Order;
using Ae.Receive.Service.Client.Request.Order;
using Ae.Receive.Service.Core.Model.Arrival;
using Ae.Receive.Service.Dal.Model;

namespace Ae.Receive.Service.Imp.Services
{
    public class ShopReceiveService: IShopReceiveService
    {
        private readonly IShopArrivalOrderRepository _shopArrivalOrderRepository;
        private readonly IShopReceiveRepository _shopReceiveRepository;
        private readonly IOrderClient _orderClient;
        private readonly IBaoYangClient _baoYangClient;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ShopReceiveService(IShopArrivalOrderRepository shopArrivalOrderRepository,
            IShopReceiveRepository shopReceiveRepository, IOrderClient orderClient, IBaoYangClient baoYangClient)
        {
            _shopArrivalOrderRepository = shopArrivalOrderRepository;
            _shopReceiveRepository = shopReceiveRepository;
            _orderClient = orderClient;
            _baoYangClient = baoYangClient;
        }

        /// <summary>
        /// 历史到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<HistoryArrivalVo>> GetHistoryReceive(HistoryReceiveRequest request)
        {
            List<HistoryArrivalVo> result = new List<HistoryArrivalVo>();
            //var reserveTypeEnum = await _baoYangClient.GetServiceTypeEnum() ?? new List<ServiceTypeEnumDto>();
            var receiveList = await _shopReceiveRepository.GetHistoryReceiveByUserId(request.UserId);
            if (receiveList != null && receiveList.Any())
            {
                List<OrderProductDto> orderProduct = new List<OrderProductDto>();
                var recIds = receiveList.Select(_ => _.Id).ToList();
                var orderList = await _shopArrivalOrderRepository.GetReceiveOrderByRecIds(recIds) ??
                                new List<ShopArrivalOrderDO>();
                if (orderList.Any())
                {
                    var orderIds = orderList.Select(_ => _.OrderNo).Distinct().ToList();
                    orderProduct = await _orderClient.GetOrderProduct(new OrderProductRequest()
                    {
                        OrderNos = orderIds
                    }) ?? new List<OrderProductDto>();
                }

                receiveList = receiveList.OrderByDescending(_ => _.Id).ToList();
                receiveList.ForEach(_ =>
                {
                    var relationOrder = orderList.Where(x => x.ArrivalId == _.Id).Select(x => x.OrderNo).Distinct()
                        .ToList();
                    var itemReceive = new HistoryArrivalVo
                    {
                        ServiceType = _.ServiceType,
                        CarNo = _.CarNo,
                        ShowArrivalTime = _.ArrivalTime.ToString("yyyy-MM-dd"),
                        ProjectItems = orderProduct.Where(t => relationOrder.Contains(t.OrderNo)).Select(c =>
                            new ProjectItemVo
                            {
                                OrderId = c.OrderNo,
                                Id = c.ProductId,
                                Name = c.ProductName,
                                Num = c.TotalNumber,
                                Price = c.TotalAmount.ToString()
                            }).ToList()
                    };
                    result.Add(itemReceive);
                });
            }

            return result;
        }

        /// <summary>
        /// 根据到店记录Id查询到店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveVo>> GetReceiveByIds(ReceiveByIdsRequest request)
        {
            var result = await _shopReceiveRepository.GetReceiveListByIds(request.ArriveIdList);

            return result?.Select(_ => new ShopReceiveVo
            {
                RecId = _.Id,
                CarPlate = _.CarNo,
                Vehicle = _.Vehicle,
                Brand = _.Brand,
                ArriveTime = _.ArrivalTime
            })?.ToList() ?? new List<ShopReceiveVo>();
        }

        /// <summary>
        /// 历史到店
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<HistoryArrivalVo>> GetHistoryReceiveNoProject(string userId)
        {
            var receiveList = await _shopReceiveRepository.GetHistoryReceiveByUserId(userId);

            return receiveList?.Select(_ => new HistoryArrivalVo
            {
                ShowArrivalTime = _.ArrivalTime.ToString("yyyy-MM-dd HH:mm:ss"),
                ServiceType = _.ServiceType,
                CarNo = _.CarNo,
                RecId = _.Id,
                TechName = _.TechName
            })?.ToList() ?? new List<HistoryArrivalVo>();
        }
    }
}
