using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface ITechTripRecordRepository : IRepository<TechTripRecordDO>
    {

        /// <summary>
        /// 查询门店车辆列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<TechTripRecordModel>> GetTripRecordPageList(GetTripRecordPageListRequest request);


        /// <summary>
        /// 查询技术出行记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TechTripRecordModel> GetTechTripRecordInfo(BaseGetInfoRequest request);
    }
}
