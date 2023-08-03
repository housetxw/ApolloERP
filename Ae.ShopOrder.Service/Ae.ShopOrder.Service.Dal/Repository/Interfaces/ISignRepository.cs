using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    /// <summary>
    /// 签收仓储
    /// </summary>
    public interface ISignRepository : IRepository<SignDO>
    {
        /// <summary>
        /// 得到签收主表信息
        /// </summary>
        /// <param name="signCode"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<SignDO> GetSign(string signCode, int shopId);


        /// <summary>
        /// 得到已经签收的包裹信息
        /// </summary>
        /// <param name="signCode"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<SignPackageDO>> GetHaveSignPackageRelationNo(string signCode, int shopId);

        /// <summary>
        /// 得到今日签收包裹列表
        /// </summary>
        Task<PagedEntity<SignDetailDO>> GetSignDetailList(int shopId, int pageSize, int pageIndex);

        /// <summary>
        /// 今日签收进度
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<TodayReceiveDO>> GetTodayReceiveOrder(int shopId);

        /// <summary>
        ///用户签收
        /// </summary>
        /// <param name="signDo"></param>
        /// <param name="signDetail"></param>
        /// <returns></returns>
        Task<long> SaveSign(SignDO signDo, List<SignDetailDO> signDetail);

        /// <summary>
        /// 得到用户已签收记录
        /// </summary>
        /// <param name="packageNo"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<SignDetailDO>> GetHaveSignDetail(List<string> packageNos, int shopId,string transferId="");

        /// <summary>
        /// 更新签收记录表状态
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        Task<long> UpdateSignStatus(string relationId, string updateBy, int signStatus, int signNum);


    }
}
