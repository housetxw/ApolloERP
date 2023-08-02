using AutoMapper;
using Ae.B.User.Api.Client.Clients;
using Ae.B.User.Api.Client.Request.Reserve;
using Ae.B.User.Api.Core.Interfaces;
using Ae.B.User.Api.Core.Request.Reserve;
using Ae.B.User.Api.Core.Response.Reserve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.User.Api.Imp.Services
{
    public class ReserveService : IReserveService
    {
        private readonly ReserveClient reserveClient;
        private readonly IMapper mapper;

        public ReserveService(ReserveClient reserveClient, IMapper mapper)
        {
            this.reserveClient = reserveClient;
            this.mapper = mapper;
        }

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        public async Task<ReserveDateForWebResponse> GetReserveDateForWeb(ReserveDateForWebRequest request)
        {
            ReserveDateForWebResponse data = new ReserveDateForWebResponse();

            var result = await reserveClient.GetReserveDateForWeb(new ReserveDateClientRequest
            {
                ShopId = request.ShopId
            });

            if (result != null)
            {
                data.ReserveDateInfoList = mapper.Map<List<ReserveDateVo>>(result);
            }

            data.DatePartList = data.ReserveDateInfoList.Select(_ => _.Date).ToList();

            data.TimePartList = data.ReserveDateInfoList.FirstOrDefault()?.Items?.Select(t => t.DatePart)?.ToList() ?? new List<string>();

            return data;
        }
    }
}
